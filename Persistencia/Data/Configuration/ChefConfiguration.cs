using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class ChefConfiguration : IEntityTypeConfiguration<Chef>
{
    public void Configure(EntityTypeBuilder<Chef> builder)
    {
        builder.ToTable("Chef");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.NombreChef)
        .IsRequired()
        .HasMaxLength(50);

        builder.Property(p => p.EspecialidadChef)
        .IsRequired()
        .HasMaxLength(50);
    }
}