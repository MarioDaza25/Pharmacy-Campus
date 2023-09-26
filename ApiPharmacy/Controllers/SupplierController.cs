using ApiPharmacy.Dtos;
using Application.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class SupplierController : BaseApiController
{

  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;

  public SupplierController(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  [HttpGet]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierDto>>> Get()
  {
    var suppliers = await _unitOfWork.People.GetAllAsync();
    return _mapper.Map<List<SupplierDto>>(suppliers);
  }

  [HttpGet("TotalSupplierGain/{year}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SupplierGainDto>>> Get2(int year)
  {
    var suppliers = await _unitOfWork.People.TotalSupplierGain(year);
    return _mapper.Map<List<SupplierGainDto>>(suppliers);
  }

  [HttpGet("GetSupplierNeverSell/{date}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<SalePatientProdDto>>> Get3(int date)
  {
      var patients = await _unitOfWork.People.GetSupplierNeverSell(date);
      return _mapper.Map<List<SalePatientProdDto>>(patients);
  }

  [HttpGet("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Get(int id)
  {
    var supplier = await _unitOfWork.People.GetByIdAsync(id);
    return Ok(supplier);
  }

  [HttpPost]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<Person>> Post(SupplierDto supplierDto)
  {
    var supplier = _mapper.Map<Person>(supplierDto);
    _unitOfWork.People.Add(supplier);
    await _unitOfWork.SaveAsync();
    if (supplierDto == null)
    {
      return BadRequest();
    }
    supplierDto.Id = supplier.Id;
    return CreatedAtAction(nameof(Post), new { id = supplierDto.Id }, supplierDto);
  }


  [HttpPut("{id}")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<SupplierDto>> Put(int id, [FromBody] SupplierDto supplierDto)
  {
    if (supplierDto == null)
    {
      return NotFound();
    }
    var supplier = _mapper.Map<Person>(supplierDto);
    _unitOfWork.People.Update(supplier);
    await _unitOfWork.SaveAsync();

    return supplierDto;
  }

  [HttpDelete("{id}")]
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Delete(int id)
  {
    var supplier = await _unitOfWork.People.GetByIdAsync(id);
    if (supplier == null)
    {
      return NotFound();
    }

    _unitOfWork.People.Remove(supplier);
    await _unitOfWork.SaveAsync();

    return NoContent();
  }

}
