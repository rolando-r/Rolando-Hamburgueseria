

using Dominio.Entities;

namespace Dominio.Interfaces;
using System.Linq.Expressions;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync1(int pageIndex, int pageSize);
    Task<IEnumerable<T>> GetAllAsync2(int pageIndex, int pageSize);
    Task<IEnumerable<T>> GetAllAsync3(int pageIndex, int pageSize);
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync1(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync2(int pageIndex, int pageSize, string search);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync3(int pageIndex, int pageSize, string search);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}

