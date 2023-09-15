namespace API.Dtos;
public class CategoriaDto
{
    public int Id { get; set; }
    public string NombreCategoria { get; set; }
    public string Descripcion { get; set; }
    public ICollection<HamburguesaDto> Hamburguesas { get; set; }
}