using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class ChefRepository : GenericRepository<Chef>, IChefRepository
{
     private readonly DbAppContext _context;

    public ChefRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }

    // Encontrar todos los chefs que se especializan en “Carnes”
    public override async Task<(int totalRegistros, IEnumerable<Chef> registros)> GetAllAsync2(int pageIndex, int pageSize, string search)
    {
        var query = _context.Chefs as IQueryable<Chef>;

        query = query.Where(p => p.EspecialidadChef == "Carnes");

        var totalRegistros = await query.CountAsync();

        var registros = await query
            .OrderBy(p => p.EspecialidadChef)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRegistros, registros);
    }
}

