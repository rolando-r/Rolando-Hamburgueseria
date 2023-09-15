using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class IngredienteConfiguration : IEntityTypeConfiguration<Ingrediente>
{
    public void Configure(EntityTypeBuilder<Ingrediente> builder)
    {
        builder.ToTable("Ingrediente");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreIngrediente)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.DescripcionIngrediente)
        .IsRequired()
        .HasMaxLength(200);

        builder.Property(p => p.PrecioIngrediente)
        .IsRequired()
        .HasColumnType("int");

        builder.Property(p => p.Stock)
        .IsRequired()
        .HasColumnType("int");
    }
}