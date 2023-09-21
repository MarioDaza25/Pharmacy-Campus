using ApiPharmacy.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiPharmacy.Controllers;

public class EmployeeController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
    {
        var employees = await _unitOfWork.People.GetAllAsync();
        return _mapper.Map<List<EmployeeDto>>(employees);
    }
}
