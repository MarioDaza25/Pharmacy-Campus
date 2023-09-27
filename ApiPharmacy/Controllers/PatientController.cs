using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class PatientController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PatientController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    [HttpGet("{product}")]
    //[Authorize(Roles = "Employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
     public async Task<IEnumerable<SalePatientProdDto>> Get(string product)
     {
        var patients = await _unitOfWork.People.GetSalePatientProduct(product);
        return _mapper.Map<List<SalePatientProdDto>>(patients);
     }

    [HttpGet("{product}/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IEnumerable<SalePatientProdDto>> Get2(string product, int date)
    {
    var patients = await _unitOfWork.People.GetSalePatientProductYear(product, date);
    return _mapper.Map<List<SalePatientProdDto>>(patients);
    }

    [HttpGet("GetPatientsNeverBuy/{date}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SalePatientProdDto>>> Get2(int date)
    {
        var patients = await _unitOfWork.People.GetPatientsNeverBuy(date);
        return _mapper.Map<List<SalePatientProdDto>>(patients);
    }


    [HttpGet("TotalSpentPatient/{year}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<SpentPatientDto>>> Get3(int year)
    {
        var patients = await _unitOfWork.People.TotalSpentPatient(year);
        return _mapper.Map<List<SpentPatientDto>>(patients);
    }
    
}