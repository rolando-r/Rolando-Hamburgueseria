namespace API.Dtos;
public class HamburguesaDto
{
    public int Id { get; set; }
    public string NombreHamburguesa { get; set; }
    public int IdCategoria { get; set; }
    public string DescripcionCategoria { get; set; }
    public int PrecioHamburguesa { get; set; }
    public int IdChef { get; set; }
    public ICollection<IngredienteDto> Ingredientes { get; set; } = new HashSet<IngredienteDto>();

}