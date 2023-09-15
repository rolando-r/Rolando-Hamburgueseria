using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class ChefController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChefController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<ChefDto>>> Get()
    {
        var chefs = await _unitOfWork.Chefs.GetAllAsync();
        return _mapper.Map<List<ChefDto>>(chefs);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ChefDto>>> Get11([FromQuery] Params chefParams)
    {
        var chef = await _unitOfWork.Chefs.GetAllAsync(chefParams.PageIndex,chefParams.PageSize,chefParams.Search);
        var lstChefsDto = _mapper.Map<List<ChefDto>>(chef.registros);
        return new Pager<ChefDto>(lstChefsDto,chef.totalRegistros,chefParams.PageIndex,chefParams.PageSize,chefParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ChefDto>> Get(int id)
    {
        var chef = await _unitOfWork.Chefs.GetByIdAsync(id);
        if (chef == null){
            return NotFound();
        }
        return _mapper.Map<ChefDto>(chef);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Chef>> Post(ChefDto chefDto){
        var chef = _mapper.Map<Chef>(chefDto);
        this._unitOfWork.Chefs.Add(chef);
        await _unitOfWork.SaveAsync();
        if (chef == null)
        {
            return BadRequest();
        }
        chefDto.Id = chef.Id;
        return CreatedAtAction(nameof(Post),new {id= chefDto.Id}, chefDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ChefDto>> Put(int id, [FromBody]ChefDto chefDto){
        if(chefDto == null)
            return NotFound();
        var chefs = _mapper.Map<Chef>(chefDto);
        _unitOfWork.Chefs.Update(chefs);
        await _unitOfWork.SaveAsync();
        return chefDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var chef = await _unitOfWork.Chefs.GetByIdAsync(id);
        if(chef == null){
            return NotFound();
        }
        _unitOfWork.Chefs.Remove(chef);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    //Endpoint para encontrar los chefs de carnes
    [HttpGet("Carnes")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ChefDto>>> Get121([FromQuery] Params chefParams)
    {
        var chef = await _unitOfWork.Chefs.GetAllAsync2(chefParams.PageIndex,chefParams.PageSize,chefParams.Search);
        var lstChefsDto = _mapper.Map<List<ChefDto>>(chef.registros);
        return new Pager<ChefDto>(lstChefsDto,chef.totalRegistros,chefParams.PageIndex,chefParams.PageSize,chefParams.Search);
    }
}