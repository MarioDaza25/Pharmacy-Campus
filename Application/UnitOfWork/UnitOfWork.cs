using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public readonly PharmacyContext _context; 
    
    public AddressRepository _adress;
    public CityRepository _city;
    public CountryRepository _country;
    public EmailRepository _email;
    public EmailTypeRepository _emailType;
    public JobTitleRepository _jobTitle;
    public NeighborhoodRepository _neighborhood;
    public PaymentMethodRepository _paymentMethod;
    public PersonRepository _person;
    public PersonTypeRepository _personType;
    public ProductRepository _product;
    public PurchaseRepository _purchase;
    public RecipeProductRepository _recipeProduct;
    public RecipeRepository _recipe;
    public RefreshTokenRepository _refreshToken;
    public RoleRepository _role;
    public SaleProductRepository _saleProduct;
    public SaleRepository _sale;
    public StateRepository _state;
    public TelephoneRepository _telephone;
    public TelephoneTypeRepository _telephoneType;
    public UserRepository _user;


    public UnitOfWork(PharmacyContext context)
    {
        _context = context;
    }



    public IUser Users
    {
        get
        {
            if (_user is not null)
            {
                return _user;
            }
            return _user = new UserRepository(_context);
        }
    }

    public IAddress Addresses => throw new NotImplementedException();

    public ICity Cities => throw new NotImplementedException();

    public ICountry Countries => throw new NotImplementedException();

    public IEmail Emails => throw new NotImplementedException();

    public IEmailType EmailTypes => throw new NotImplementedException();

    public IIdentificationType IdentificationTypes => throw new NotImplementedException();

    public IJobTitle JobTitles => throw new NotImplementedException();

    public INeighborhood Neighborhoods => throw new NotImplementedException();

    public IPaymentMethod PaymentMethods => throw new NotImplementedException();

    public IPerson People => throw new NotImplementedException();

    public IPersonType PersonTypes => throw new NotImplementedException();

    public IProduct Products => throw new NotImplementedException();

    public IPurchase Purchases => throw new NotImplementedException();

    public IRecipe Recipes => throw new NotImplementedException();

    public IRecipeProduct RecipeProducts => throw new NotImplementedException();

    public ISale Sales => throw new NotImplementedException();

    public ISaleProduct SaleProducts => throw new NotImplementedException();

    public IState States => throw new NotImplementedException();

    public ITelephone Telephones => throw new NotImplementedException();

    public ITelephoneType TelephoneTypes => throw new NotImplementedException();

    public IRole Roles => throw new NotImplementedException();

    public IRefreshToken RefreshTokens => throw new NotImplementedException();

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    } 
}
