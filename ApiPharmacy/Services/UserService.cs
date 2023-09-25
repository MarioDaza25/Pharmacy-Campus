using ApiPharmacy.Dtos;
using ApiPharmacy.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ApiPharmacy.Services;

public class UserService
{
    public readonly JWT _jwt;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPasswordHasher<User> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> RegisterAsync(RegisterDto registerDto)
    {
        var user = new User
        {
            Email = registerDto.Email,
            Username = registerDto.Name
        };

        user.Password = _passwordHasher.HashPassword(user, registerDto.Password); //aqui sucede el hasheo o la encriptacion
        var existingUser = _unitOfWork.Users
                                    .Find(u => u.Username.ToLower() == registerDto.Name.ToLower())
                                    .FirstOrDefault();
        if (existingUser == null)
        {
            var roleDefault = _unitOfWork.JobTitles//=> Roles
                                        .Find(u => u.Description == Authorization.rol_default.ToString())
                                        .First();
            try
            {
                user.JobsTitle.Add(roleDefault);
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


}