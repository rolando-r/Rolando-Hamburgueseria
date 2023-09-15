

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
         IUsuario Usuarios {get;}
         IRol Roles {get;}
        Task<int> SaveAsync();
    }

