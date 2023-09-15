using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class IngredienteRepository : GenericRepository<Ingrediente>, IIngredienteRepository
{
     private readonly DbAppContext _context;

    public IngredienteRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    // Encontrar el ingrediente más caro
    public override async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Ingredientes as IQueryable<Ingrediente>;
        
        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderByDescending(p => p.PrecioIngrediente) // Encontrar el Ingrediente mas caro
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros.Take(1));
    }
   
   // Encontrar todos los ingredientes cuyo precio sea entre $2 y $5
   public override async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetAllAsync1(int pageIndex, int pageSize, string search)
    {
        var query = _context.Ingredientes as IQueryable<Ingrediente>;

        query = query.Where(p => p.PrecioIngrediente >= 2 && p.PrecioIngrediente <= 5);

        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.PrecioIngrediente) // Ordenar por PrecioIngrediente en orden de precio
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    // Encontrar todos los ingredientes con stock menor a 400.
    public override async Task<(int totalRegistros, IEnumerable<Ingrediente> registros)> GetAllAsync2(int pageIndex, int pageSize, string search)
    {
        var query = _context.Ingredientes as IQueryable<Ingrediente>;

        query = query.Where(p => p.Stock < 400);

        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.Stock) // Ordenar por Stock en orden
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}
