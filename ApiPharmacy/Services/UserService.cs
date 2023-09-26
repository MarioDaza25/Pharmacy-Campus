using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ApiPharmacy.Dtos;
using ApiPharmacy.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiPharmacy.Services;
//pasos para la creacion de los metodos necesarios para el uso de JWT
public class UserService :IUserService
{   
    //crear los atributos y el conectarlos con el constructor
    public readonly JWT _jwt;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<User> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }
    //creacion del primer metodo (register)
    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        //crear una instancia de usuario y hacer el mapping
        var user = new User
        {
            Email = registerDto.Email,
            Username = registerDto.Name
        };
        // pasar la contraseña hasheada a la instacia de usuario
        user.Password = _passwordHasher.HashPassword(user, registerDto.Password); //aqui sucede el hasheo o la encriptacion
        // vericar que la instacia usuario y sus atributos existan
        var existingUser = _unitOfWork.Users
                                    .Find(u => u.Username.ToLower() == registerDto.Name.ToLower())
                                    .FirstOrDefault();
        //si no existe le asigna un rol a la instancia usuario
        if (existingUser == null)
        {
            /* var roleDefault = _unitOfWork.JobTitles//=> Roles
                                        .Find(u => u.Description == Authorization.rol_default.ToString())
                                        .First(); */
            try
            {
                //si no existe, se hace la persistencia de la entidad
               /*  user.JobsTitle.Add(roleDefault); */
                _unitOfWork.Users.Add(user);
                await _unitOfWork.SaveAsync();

                return $"User {registerDto.Name} has been registered succesfully";
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                return $"Error: {message}";
            }
        }
        else
        {
            return $"User {registerDto.Name} Already registered.";
        }

    }

        public async Task<DataUserDto> GetTokenAsync(LoginDto model)
        {
            //crear la instacia principal del metodo
            DataUserDto dataUserDto = new DataUserDto();
            //obtener el usuario vinculado al token
            var user  = await _unitOfWork.Users
                                .GetByUsernameAsync(model.Name);
            
            //verificar si el usuario esta autenticado
            if(user == null)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"User does not exist with username {model.Name}";
                return dataUserDto;
            }
            // compara la contraseña hasheada y guardada en la base de datos, con la que pasa el usuario en el login
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password,model.Password);
            // si la contraseña es igual a la almacenada en la bd, se crea un token unico, y se llenan los atributos para datauserdto
            if(result == PasswordVerificationResult.Success)
            {
                dataUserDto.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
            
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.Email = user.Email;
                dataUserDto.Name = user.Username;
                dataUserDto.JobTitle = user.JobsTitle
                                        .Select(u => u.Description)
                                       .ToList();

                //si el usuario tiene algun refreshtoken activo, lo guarda y lo pasa a los datos de usario                   
                if(user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens.Where(a => a.IsActive == true).FirstOrDefault();
                    dataUserDto.RefreshToken = activeRefreshToken.Token;
                    //le pasa la fecha de expiracion real del refresh token a los datos de usuario
                    dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
                }  
                else
                {
                    //si no hay ningun token activo lo crea y le hace la persistencia en la unidad de trabajo
                    var refreshToken = CreateRefreshToken();
                    dataUserDto.RefreshToken = refreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.SaveAsync();
                }                 

                return dataUserDto;
            }
            // si el hasheo no conincide, lanza este mensaje
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Credenciales incorrectas para el usuario{user.Username}";
            return dataUserDto;
        }

        private RefreshToken CreateRefreshToken()
        {
            // crea un numero aleatorio de 32 bytes
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                //Llena los atributos de la instacia refresh y lo devuelve 
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }
        public async Task<string> AddRoleAsync(AddRoleDto model)
    {

        var user = await _unitOfWork.Users
                    .GetByUsernameAsync(model.Username);
        if (user == null)
        {
            return $"User {model.Username} does not exists.";
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, model.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var rolExists = _unitOfWork.JobTitles
                                        .Find(u => u.Description.ToLower() == model.Role.ToLower())
                                        .FirstOrDefault();

            if (rolExists != null)
            {
                var userHasRole = user.JobsTitle
                                            .Any(u => u.Id == rolExists.Id);

                if (userHasRole == false)
                {
                    user.JobsTitle.Add(rolExists);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.SaveAsync();
                }

                return $"Role {model.Role} added to user {model.Username} successfully.";
            }

            return $"Role {model.Role} was not found.";
        }
        return $"Invalid Credentials";
    }
    public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
    {
        var dataUserDto = new DataUserDto();

        var usuario = await _unitOfWork.Users
                        .GetByRefreshTokenAsync(refreshToken);

        if (usuario == null)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not assigned to any user.";
            return dataUserDto;
        }

        var refreshTokenBd = usuario.RefreshTokens.Single(x => x.Token == refreshToken);

        if (!refreshTokenBd.IsActive)
        {
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Token is not active.";
            return dataUserDto;
        }
        //Revoque the current refresh token and
        refreshTokenBd.Revoked = DateTime.UtcNow;
        //generate a new refresh token and save it in the database
        var newRefreshToken = CreateRefreshToken();
        usuario.RefreshTokens.Add(newRefreshToken);
        _unitOfWork.Users.Update(usuario);
        await _unitOfWork.SaveAsync();
        //Generate a new Json Web Token
        dataUserDto.IsAuthenticated = true;
        JwtSecurityToken jwtSecurityToken = CreateJwtToken(usuario);
        dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        dataUserDto.Email = usuario.Email;
        dataUserDto.Name = usuario.Username;
        dataUserDto.JobTitle = usuario.JobsTitle
                                        .Select(u => u.Description)
                                        .ToList();
        dataUserDto.RefreshToken = newRefreshToken.Token;
        dataUserDto.RefreshTokenExpiration = newRefreshToken.Expires;
        return dataUserDto;
    }
     private JwtSecurityToken CreateJwtToken(User usuario)
    {
        var jobsTitle = usuario.JobsTitle;
        var roleClaims = new List<Claim>();
        foreach (var jobTitle in jobsTitle)
        {
            roleClaims.Add(new Claim("roles", jobTitle.Description));
        }
        var claims = new[]
        {
                                new Claim(JwtRegisteredClaimNames.Sub, usuario.Username),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                                new Claim("uid", usuario.Id.ToString())
                        }
        .Union(roleClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }


}