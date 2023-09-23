<<<<<<< HEAD
using ApiJwt.Dtos;
=======
>>>>>>> f4a3fadce8a7051146e98d06efe41a55bc61f8ff
using ApiPharmacy.Dtos;

namespace ApiPharmacy.Services;

public interface IUserService
{
<<<<<<< HEAD
    Task<string> RegisterToken(RegisterDto model);
=======
    Task<string> RegisterAsync(RegisterDto model);
>>>>>>> f4a3fadce8a7051146e98d06efe41a55bc61f8ff
    Task<string> AddRoleAsync(AddRoleDto model); 
    Task<DataUserDto> GetTokenAsync(LoginDto model);
    Task<DataUserDto> RefreshTokenAsync(string refreshToken);
}