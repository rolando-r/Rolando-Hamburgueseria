namespace Dominio.Entities;
public class Ingrediente : BaseEntity
{
    public string NombreIngrediente { get; set; }
    public string DescripcionIngrediente { get; set; }
    public int PrecioIngrediente { get; set; }
    public int Stock { get; set; }
    public ICollection<HamburguesaIngredientes> HamburguesasIngredientes { get; set; }
    public ICollection<Hamburguesa> Hamburguesas { get; set; } = new HashSet<Hamburguesa>();
}