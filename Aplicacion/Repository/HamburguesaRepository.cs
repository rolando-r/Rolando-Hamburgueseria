using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class HamburguesaRepository : GenericRepository<Hamburguesa>, IHamburguesaRepository
{
     private readonly DbAppContext _context;

    public HamburguesaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<Hamburguesa> GetByIdAsync(int id)
        {
            return await _context.Hamburguesas
            .Include(p => p.Ingredientes)
            .FirstOrDefaultAsync(x => x.Id == id);
        } 
   
   // Ordenar las hamburguesas por su precio. El end point esta en la 1.1, paginacion
    public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;
        
        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.PrecioHamburguesa) // Ordenar por PrecioHamburguesa en orden de precio
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync1(int pageIndex, int pageSize, string search)
    {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;

        // Filtrar por precio menor o igual a $9
        query = query.Where(p => p.PrecioHamburguesa <= 9);

        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.PrecioHamburguesa) // Ordenar por PrecioHamburguesa en orden de precio
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }

    // Encontrar todas las hamburguesas de la categoría “Vegetariana”
    public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync2(int pageIndex, int pageSize, string search)
    {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;

        query = query.Where(p => p.DescripcionCategoria == "Vegetariana");

        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.DescripcionCategoria)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
       
    // funcion para ver las hamburguesas que ha hecho un chef en especifico (No funciona)
    
    /* public override async Task<(int totalRegistros, IEnumerable<Hamburguesa> registros)> GetAllAsync3(int pageIndex, int pageSize, string search, int idChef) {
        var query = _context.Hamburguesas as IQueryable<Hamburguesa>;
        query = query.Where(p => p.Chef == NombreChef);
        var totalRegistros = await query.CountAsync();
        var registros = await query
            .OrderBy(p => p.Chef)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    } */
}
