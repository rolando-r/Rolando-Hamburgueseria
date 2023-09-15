namespace API.Dtos;
public class IngredienteDto
{
    public int Id { get; set; }
    public string NombreIngrediente { get; set; }
    public string DescripcionIngrediente { get; set; }
    public int PrecioIngrediente { get; set; }
    public int Stock { get; set; }
    public ICollection<HamburguesaDto> Hamburguesas { get; set; } = new HashSet<HamburguesaDto>();
}