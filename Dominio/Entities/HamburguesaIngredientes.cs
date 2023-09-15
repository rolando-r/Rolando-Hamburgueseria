namespace Dominio.Entities;
public class HamburguesaIngredientes
{
    public int IdHamburguesa { get; set; }
    public Hamburguesa Hamburguesa { get; set; }
    public int IdIngrediente { get; set; }
    public Ingrediente Ingrediente { get; set; }
}