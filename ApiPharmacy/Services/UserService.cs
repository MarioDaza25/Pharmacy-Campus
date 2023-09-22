namespace ApiPharmacy.Services;

public class UserService
{
    public readonly JWT _jwt;
    public readonly IUnitOfWork _unitOfWork;
    public readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUnitOfWork unitOfWork, IOptions<JWT> jwt, IPassword<User> passwordHasher)
    {
        _jwt = jwt.Value;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public async Task<string RegisterAsync(RegisterDto registerDto)
    {}
    //build : construccion de jwt en proceso, me lo dejan a mi, no avancen, igualmente no afecta el flujo de ejecucion del programa
}