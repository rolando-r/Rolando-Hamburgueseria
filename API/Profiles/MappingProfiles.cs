using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;
public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Categoria,CategoriaDto>().ReverseMap();
        CreateMap<Chef,ChefDto>().ReverseMap();
        CreateMap<Hamburguesa,HamburguesaDto>().ReverseMap();
        CreateMap<Ingrediente,IngredienteDto>().ReverseMap();
    }
}