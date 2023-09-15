using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");
            builder.Property(p => p.Id)
            .IsRequired();

            builder.Property(p => p.NombreCategoria)
            .IsRequired()
            .HasMaxLength(50);

            builder.Property(p => p.Descripcion)
            .IsRequired()
            .HasMaxLength(200);
        }
    }
}