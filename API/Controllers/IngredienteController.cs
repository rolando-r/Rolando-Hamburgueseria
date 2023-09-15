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

public class IngredienteController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public IngredienteController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<IngredienteDto>>> Get()
    {
        var ingredientes = await _unitOfWork.Ingredientes.GetAllAsync();
        return _mapper.Map<List<IngredienteDto>>(ingredientes);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<IngredienteDto>>> Get121([FromQuery] Params ingredienteParams)
    {
        var ingrediente = await _unitOfWork.Ingredientes.GetAllAsync(ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
        var lstIngredientesDto = _mapper.Map<List<IngredienteDto>>(ingrediente.registros);
        return new Pager<IngredienteDto>(lstIngredientesDto,ingrediente.totalRegistros,ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IngredienteDto>> Get(int id)
    {
        var ingrediente = await _unitOfWork.Ingredientes.GetByIdAsync(id);
        if (ingrediente == null){
            return NotFound();
        }
        return _mapper.Map<IngredienteDto>(ingrediente);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Ingrediente>> Post(IngredienteDto ingredienteDto){
        var ingrediente = _mapper.Map<Ingrediente>(ingredienteDto);
        this._unitOfWork.Ingredientes.Add(ingrediente);
        await _unitOfWork.SaveAsync();
        if (ingrediente == null)
        {
            return BadRequest();
        }
        ingredienteDto.Id = ingrediente.Id;
        return CreatedAtAction(nameof(Post),new {id= ingredienteDto.Id}, ingredienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Put(int id, [FromBody]IngredienteDto ingredienteDto){
        if(ingredienteDto == null)
            return NotFound();
        var ingredientes = _mapper.Map<Ingrediente>(ingredienteDto);
        _unitOfWork.Ingredientes.Update(ingredientes);
        await _unitOfWork.SaveAsync();
        return ingredienteDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var ingrediente = await _unitOfWork.Ingredientes.GetByIdAsync(id);
        if(ingrediente == null){
            return NotFound();
        }
        _unitOfWork.Ingredientes.Remove(ingrediente);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    //End Points Percio entre 2 y 25
    [HttpGet("Precio2a5")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<IngredienteDto>>> Get112([FromQuery] Params ingredienteParams)
    {
        var ingrediente = await _unitOfWork.Ingredientes.GetAllAsync1(ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
        var lstIngredientesDto = _mapper.Map<List<IngredienteDto>>(ingrediente.registros);
        return new Pager<IngredienteDto>(lstIngredientesDto,ingrediente.totalRegistros,ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
    }

    // Endpoint para ver el stock menor de 400
    [HttpGet("StockMenor400")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<IngredienteDto>>> Get113([FromQuery] Params ingredienteParams)
    {
        var ingrediente = await _unitOfWork.Ingredientes.GetAllAsync2(ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
        var lstIngredientesDto = _mapper.Map<List<IngredienteDto>>(ingrediente.registros);
        return new Pager<IngredienteDto>(lstIngredientesDto,ingrediente.totalRegistros,ingredienteParams.PageIndex,ingredienteParams.PageSize,ingredienteParams.Search);
    }

    // EndPoint para cambiar la descripcion de pan

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IngredienteDto>> Put2(int id, [FromBody]IngredienteDto ingredienteDto)
    {
        if (ingredienteDto == null) return NotFound();

        if (ingredienteDto.DescripcionIngrediente == null)
        {
            return BadRequest("El campo descripcion es obligatorio.");
        }

        // If para cambiar la descripción del "Pan" a "Pan fresco y crujiente". (Sin probar)
        if (ingredienteDto.NombreIngrediente == "Pan")
        {
            ingredienteDto.DescripcionIngrediente = "Pan fresco y crujiente";
        }

        var ingredientes = _mapper.Map<Ingrediente>(ingredienteDto);
        _unitOfWork.Ingredientes.Update(ingredientes);
        await _unitOfWork.SaveAsync();

        return ingredienteDto;
}
}