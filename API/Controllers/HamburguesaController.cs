using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class HamburguesaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HamburguesaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<HamburguesaDto>>> Get()
    {
        var hamburguesas = await _unitOfWork.Hamburguesas.GetAllAsync();
        return _mapper.Map<List<HamburguesaDto>>(hamburguesas);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaDto>>> Get11([FromQuery] Params hamburguesaParams)
    {
        var hamburguesa = await _unitOfWork.Hamburguesas.GetAllAsync(hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
        var lstHamburguesasDto = _mapper.Map<List<HamburguesaDto>>(hamburguesa.registros);
        return new Pager<HamburguesaDto>(lstHamburguesasDto,hamburguesa.totalRegistros,hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<HamburguesaDto>> Get(int id)
    {
        var hamburguesa = await _unitOfWork.Hamburguesas.GetByIdAsync(id);
        if (hamburguesa == null){
            return NotFound();
        }
        return _mapper.Map<HamburguesaDto>(hamburguesa);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Hamburguesa>> Post(HamburguesaDto hamburguesaDto){
        var hamburguesa = _mapper.Map<Hamburguesa>(hamburguesaDto);
        this._unitOfWork.Hamburguesas.Add(hamburguesa);
        await _unitOfWork.SaveAsync();
        if (hamburguesa == null)
        {
            return BadRequest();
        }
        hamburguesaDto.Id = hamburguesa.Id;
        return CreatedAtAction(nameof(Post),new {id= hamburguesaDto.Id}, hamburguesaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HamburguesaDto>> Put(int id, [FromBody]HamburguesaDto hamburguesaDto){
        if(hamburguesaDto == null)
            return NotFound();
        var hamburguesas = _mapper.Map<Hamburguesa>(hamburguesaDto);
        _unitOfWork.Hamburguesas.Update(hamburguesas);
        await _unitOfWork.SaveAsync();
        return hamburguesaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var hamburguesa = await _unitOfWork.Hamburguesas.GetByIdAsync(id);
        if(hamburguesa == null){
            return NotFound();
        }
        _unitOfWork.Hamburguesas.Remove(hamburguesa);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    // Endpoint para ver las hamburguesas con un precio menor o igual a 9

    [HttpGet("MenorOIgualA9")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaDto>>> Get112([FromQuery] Params hamburguesaParams)
    {
        var hamburguesa = await _unitOfWork.Hamburguesas.GetAllAsync1(hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
        var lstHamburguesasDto = _mapper.Map<List<HamburguesaDto>>(hamburguesa.registros);
        return new Pager<HamburguesaDto>(lstHamburguesasDto,hamburguesa.totalRegistros,hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
    }

    [HttpGet("Vegetarianas")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaDto>>> Get1312([FromQuery] Params hamburguesaParams)
    {
        var hamburguesa = await _unitOfWork.Hamburguesas.GetAllAsync2(hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
        var lstHamburguesasDto = _mapper.Map<List<HamburguesaDto>>(hamburguesa.registros);
        return new Pager<HamburguesaDto>(lstHamburguesasDto,hamburguesa.totalRegistros,hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
    }

    /* [HttpGet("QuesoCheddar")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<HamburguesaDto>>> Get1212([FromQuery] Params hamburguesaParams)
    {
        var hamburguesa = await _unitOfWork.Hamburguesas.GetAllAsync3(hamburguesaParams.PageIndex, hamburguesaParams.PageSize, hamburguesaParams.Search);
        var lstHamburguesasDto = _mapper.Map<List<HamburguesaDto>>(hamburguesa.registros);
        return new Pager<HamburguesaDto>(lstHamburguesasDto,hamburguesa.totalRegistros,hamburguesaParams.PageIndex,hamburguesaParams.PageSize,hamburguesaParams.Search);
    } */
}