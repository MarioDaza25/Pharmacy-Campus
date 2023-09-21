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
    public IdentificationTypeRepository _identificationType;
    public JobTitleRepository _jobTitle;
    public NeighborhoodRepository _neighborhood;
    public PaymentMethodRepository _paymentMethod;
    public PersonRepository _person;
    public PersonTypeRepository _personType;
    public ProductRepository _product;
    public PurchaseRepository _purchase;
    public RecipeProductRepository _recipeProduct;
    public RecipeRepository _recipe;
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


    public IAddress Addresses
    {
        get
        {
            if (_adress is not null)
            {
                return _adress;
            }
            return _adress = new AddressRepository(_context);
        }
    }

    public ICity Cities
    {
        get
        {
            if (_city is not null)
            {
                return _city;
            }
            return _city = new CityRepository(_context);
        }
    }

    public ICountry Countries
    {
        get
        {
            if (_country is not null)
            {
                return _country;
            }
            return _country = new CountryRepository(_context);
        }
    }

    public IEmail Emails
    {
        get
        {
            if (_email is not null)
            {
                return _email;
            }
            return _email = new EmailRepository(_context);
        }
    }

    public IEmailType EmailTypes
    {
        get
        {
            if (_emailType is not null)
            {
                return _emailType;
            }
            return _emailType = new EmailTypeRepository(_context);
        }
    }

    public IIdentificationType IdentificationTypes
    {
        get
        {
            if (_identificationType is not null)
            {
                return _identificationType;
            }
            return _identificationType = new IdentificationTypeRepository(_context);
        }
    }

    public IJobTitle JobTitles
    {
        get
        {
            if (_jobTitle is not null)
            {
                return _jobTitle;
            }
            return _jobTitle = new JobTitleRepository(_context);
        }
    }

    public INeighborhood Neighborhoods
    {
        get
        {
            if (_neighborhood is not null)
            {
                return _neighborhood;
            }
            return _neighborhood = new NeighborhoodRepository(_context);
        }
    }

    public IPaymentMethod PaymentMethods
    {
        get
        {
            if (_paymentMethod is not null)
            {
                return _paymentMethod;
            }
            return _paymentMethod = new PaymentMethodRepository(_context);
        }
    }

    public IPerson People
    {
        get
        {
            if (_person is not null)
            {
                return _person;
            }
            return _person = new PersonRepository(_context);
        }
    }

    public IPersonType PersonTypes
    {
        get
        {
            if (_personType is not null)
            {
                return _personType;
            }
            return _personType = new PersonTypeRepository(_context);
        }
    }

    public IProduct Products
    {
        get
        {
            if (_product is not null)
            {
                return _product;
            }
            return _product = new ProductRepository(_context);
        }
    }

    public IPurchase Purchases
    {
        get
        {
            if (_purchase is not null)
            {
                return _purchase;
            }
            return _purchase = new PurchaseRepository(_context);
        }
    }

    public IRecipe Recipes
    {
        get
        {
            if (_recipe is not null)
            {
                return _recipe;
            }
            return _recipe = new RecipeRepository(_context);
        }
    }

    public IRecipeProduct RecipeProducts
    {
        get
        {
            if (_recipeProduct is not null)
            {
                return _recipeProduct;
            }
            return _recipeProduct = new RecipeProductRepository(_context);
        }
    }

    public ISale Sales
    {
        get
        {
            if (_sale is not null)
            {
                return _sale;
            }
            return _sale = new SaleRepository(_context);
        }
    }

    public ISaleProduct SaleProducts
    {
        get
        {
            if (_saleProduct is not null)
            {
                return _saleProduct;
            }
            return _saleProduct = new SaleProductRepository(_context);
        }
    }

    public IState States
    {
        get
        {
            if (_state is not null)
            {
                return _state;
            }
            return _state = new StateRepository(_context);
        }
    }

    public ITelephone Telephones
    {
        get
        {
            if (_telephone is not null)
            {
                return _telephone;
            }
            return _telephone = new TelephoneRepository(_context);
        }
    }

    public ITelephoneType TelephoneTypes
    {
        get
        {
            if (_telephoneType is not null)
            {
                return _telephoneType;
            }
            return _telephoneType = new TelephoneTypeRepository(_context);
        }
    }

    public IRole Roles
    {
        get
        {
            if (_role is not null)
            {
                return _role;
            }
            return _role = new RoleRepository(_context);
        }
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


    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    } 
}
