namespace Dominio.Entities;
public class Hamburguesa : BaseEntity
{
    public string[] Ingrediente;

    public string NombreHamburguesa { get; set; }
    public int IdCategoria { get; set; }
    public string DescripcionCategoria { get; set; }
    public Categoria Categoria { get; set; }
    public int PrecioHamburguesa { get; set; }
    public int IdChef { get; set; }
    public Chef Chef { get; set; }
    public ICollection<HamburguesaIngredientes> HamburguesasIngredientes { get; set; }
    public ICollection<Ingrediente> Ingredientes { get; set; } = new HashSet<Ingrediente>();
}