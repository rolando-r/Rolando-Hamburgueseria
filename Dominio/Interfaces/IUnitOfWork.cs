

namespace Dominio.Interfaces;

    public interface IUnitOfWork
    {
         IUsuarioRepository Usuarios {get;}
         IRolRepository Roles {get;}
         ICategoriaRepository Categorias {get;}
         IHamburguesaRepository Hamburguesas {get;}
         IIngredienteRepository Ingredientes {get;}
         IChefRepository Chefs {get;}
        Task<int> SaveAsync();
    }

