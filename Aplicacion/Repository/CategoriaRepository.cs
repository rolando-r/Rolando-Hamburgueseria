using System.Linq.Expressions;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Repository;

public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
{
     private readonly DbAppContext _context;

    public CategoriaRepository(DbAppContext context) : base(context)
    {
        _context = context;
    }
   

   //Funcion para encontrar todas las categorías que contienen la palabra “gourmet” en su descripción (No Funciona)

   /* public override async Task<(int totalRegistros, IEnumerable<Categoria> registros)> GetAllAsync2(int pageIndex, int pageSize, bool searchGourmand) 
   {
    var query = _context.Categorias as IQueryable<Categoria>;

    if (searchGourmand) {
        query = query.Where(p => p.Descripcion.Contains("gourmet"));
    }

    var totalRegistros = await query.CountAsync();
    var registros = await query
        .Skip((pageIndex - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

    if (registros.Count() == 0) {
        throw new Exception("No se encontraron categorias");
    }

    return (totalRegistros, registros);
    } */
}
