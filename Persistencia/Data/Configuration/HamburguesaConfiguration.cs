using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class HamburguesaConfiguration : IEntityTypeConfiguration<Hamburguesa>
{
    public void Configure(EntityTypeBuilder<Hamburguesa> builder)
    {
        builder.ToTable("Hamburguesa");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreHamburguesa)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.DescripcionCategoria)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasOne(p => p.Categoria)
        .WithMany(p => p.Hamburguesas)
        .HasForeignKey(p => p.IdCategoria);

        builder.HasOne(p => p.Chef)
        .WithMany(p => p.Hamburguesas)
        .HasForeignKey(p => p.IdChef);

        builder
        .HasMany(p => p.Ingredientes)
        .WithMany(p => p.Hamburguesas)
        .UsingEntity<HamburguesaIngredientes>(
            j => j
                .HasOne(pt => pt.Ingrediente)
                .WithMany(t => t.HamburguesasIngredientes)
                .HasForeignKey(p => p.IdIngrediente),
            j => j
                .HasOne(pt => pt.Hamburguesa)
                .WithMany(t => t.HamburguesasIngredientes)
                .HasForeignKey(p => p.IdHamburguesa),
            j =>
            {
                j.HasKey(pt => new {pt.IdHamburguesa, pt.IdIngrediente});
            });
    }
}