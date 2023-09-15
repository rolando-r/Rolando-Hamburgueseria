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


public class CategoriaController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async  Task<ActionResult<IEnumerable<CategoriaDto>>> Get()
    {
        var categorias = await _unitOfWork.Categorias.GetAllAsync();
        return _mapper.Map<List<CategoriaDto>>(categorias);
    }
    [HttpGet("Pager")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> Get11([FromQuery] Params categoriaParams)
    {
        var categoria = await _unitOfWork.Categorias.GetAllAsync(categoriaParams.PageIndex,categoriaParams.PageSize,categoriaParams.Search);
        var lstCategoriasDto = _mapper.Map<List<CategoriaDto>>(categoria.registros);
        return new Pager<CategoriaDto>(lstCategoriasDto,categoria.totalRegistros,categoriaParams.PageIndex,categoriaParams.PageSize,categoriaParams.Search);
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoriaDto>> Get(int id)
    {
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
        if (categoria == null){
            return NotFound();
        }
        return _mapper.Map<CategoriaDto>(categoria);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Categoria>> Post(CategoriaDto categoriaDto){
        var categoria = _mapper.Map<Categoria>(categoriaDto);
        this._unitOfWork.Categorias.Add(categoria);
        await _unitOfWork.SaveAsync();
        if (categoria == null)
        {
            return BadRequest();
        }
        categoriaDto.Id = categoria.Id;
        return CreatedAtAction(nameof(Post),new {id= categoriaDto.Id}, categoriaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CategoriaDto>> Put(int id, [FromBody]CategoriaDto categoriaDto){
        if(categoriaDto == null)
            return NotFound();
        var categorias = _mapper.Map<Categoria>(categoriaDto);
        _unitOfWork.Categorias.Update(categorias);
        await _unitOfWork.SaveAsync();
        return categoriaDto;
        
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var categoria = await _unitOfWork.Categorias.GetByIdAsync(id);
        if(categoria == null){
            return NotFound();
        }
        _unitOfWork.Categorias.Remove(categoria);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    [HttpGet("Gourmet")]
    [Authorize]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<CategoriaDto>>> Get211([FromQuery] Params categoriaParams)
    {
        var categoria = await _unitOfWork.Categorias.GetAllAsync2(categoriaParams.PageIndex,categoriaParams.PageSize,categoriaParams.Search);
        var lstCategoriasDto = _mapper.Map<List<CategoriaDto>>(categoria.registros);
        return new Pager<CategoriaDto>(lstCategoriasDto,categoria.totalRegistros,categoriaParams.PageIndex,categoriaParams.PageSize,categoriaParams.Search);
    }
}