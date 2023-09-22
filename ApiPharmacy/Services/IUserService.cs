using ApiJwt.Dtos;
using ApiPharmacy.Dtos;

namespace ApiPharmacy.Services;

public interface IUserService
{
    Task<string> RegisterToken(RegisterDto model);
    Task<string> AddRoleAsync(AddRoleDto model); 
    Task<DataUserDto> GetTokenAsync(LoginDto model);
    Task<DataUserDto> RefreshTokenAsync(string refreshToken);
}