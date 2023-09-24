using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

    
public class ProductController : BaseApiController
{
        private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
    {
        var products = await _unitOfWork.Products.GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }


    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(int id)
    {
        var products = await _unitOfWork.Products.GetByIdAsync(id);
        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Post(ProductDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveAsync();
        if (productDto == null)
        {
            return BadRequest();
        }
        productDto.Id = product.Id;
        return CreatedAtAction(nameof(Post), new { id = productDto.Id }, productDto);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductDto>> Put(int id, [FromBody] ProductDto productDto)
    {
        if (productDto == null)
        {
            return NotFound();
        }
        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();

        return productDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

        // creacion controller de product < 50
    [HttpGet("GetLessThan/{amount}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetLessThan(int amount)
    {
        var products = await _unitOfWork.Products.GetLessThanStockAsync(amount);
        if(products == null)
        {
            return NotFound("No se encontraron productos menores a " + amount );
        }
        return _mapper.Map<List<ProductDto>>(products);

    }
    // Medicamentos que caducan antes del 1 de enero de 2024.
    [HttpGet("GetProductsExpired/{expiryDate}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductExpiredBefore(DateTime ExpiryDate)
    {
        var products = await _unitOfWork.Products.GetAllProductExpiredBeforeAsync(ExpiryDate);
        if(products == null)
        {
            return NotFound("No se encontraron productos a expirar en la fecha " + ExpiryDate );
        }
        return _mapper.Map<List<ProductDto>>(products);

    }
    //Medicamentos con un precio mayor a 50 y un stock menor a 100.
    
    [HttpGet("GetHighPricedLowStock/{price}/{stock}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetHighPricedLowStock(decimal price, double stock)
    {
        var products = await _unitOfWork.Products.GetHighPricedLowStockAsync(price,stock);
        if(products == null)
        {
            return NotFound($"No se encontraron productos con un precio mayor a {price} y un stock menor a  {stock}.");
        }
        return _mapper.Map<List<ProductDto>>(products);

    }
    [HttpGet("GetAllProductsNeverSold")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsNeverSold()
    {
        var products = await _unitOfWork.Products.GetAllProductsNeverSold();
        if(products == null)
        {
            return NotFound("Actualmente todos los productos han tenido ventas ");
        }
        return _mapper.Map<List<ProductDto>>(products);
    } 

    [HttpGet("GetAllProductsNotSoldAtYear/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsNotSoldAtYear(DateTime year)
    {
        var products = await _unitOfWork.Products.GetAllProductsNotSoldInYearAsync(year);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, // Código de estado HTTP 204 (No Content)
                Content = $"El el año {year} todos los productos han tenido ventas :D",
                ContentType = "text/plain" // Tipo de contenido 
            };
        }
        return _mapper.Map<List<ProductDto>>(products);
    }

    [HttpGet("GetAllProductsSoldInMonth/{month}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsSoldInMonth(int month)
    {
        var products = await _unitOfWork.Products.GetAllProductsSoldInMonthAsync(month);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, 
                Content = $"No hay ventas de ningun producto este mes :C",
                ContentType= "text/Plain"
            };
        }
        return _mapper.Map<List<ProductDto>>(products);
    }
    [HttpGet("GetAllProductsBySupplier/{supplier}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProductsBySupplier(string supplier)
    {
        var products = await _unitOfWork.Products.GetAllProductsBySupplierAsync(supplier);
        if(products == null)
        {
            return new ContentResult
            {
                StatusCode = 204, 
                Content = $"No hay productos relacionados con el proovedor {supplier}",
                ContentType= "text/Plain"
            };
        }
        return _mapper.Map<List<ProductDto>>(products);
    }
}
