namespace API.Dtos;
public class ChefDto
{
    public int Id { get; set; }
    public string NombreChef { get; set; }
    public string EspecialidadChef { get; set; }
    public ICollection<HamburguesaDto> Hamburguesas { get; set; }
}