using Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistencia.Data.Configuration;
public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");
        builder.Property(p => p.Id)
        .IsRequired();

        builder.Property(p => p.Username)
        .IsRequired()
        .HasMaxLength(100);

        builder.HasIndex(p => new {
            p.Username,
            p.Email
        }).HasDatabaseName("IX_MiIndice")
        .IsUnique();

        builder.Property(p => p.Email)
        .IsRequired()
        .HasMaxLength(200);

        builder
        .HasMany(p => p.Roles)
        .WithMany(p => p.Usuarios)
        .UsingEntity<UsuarioRoles>(
            j => j
                .HasOne(pt => pt.Rol)
                .WithMany(t => t.UsuariosRoles)
                .HasForeignKey(p => p.RolId),
            j => j
                .HasOne(pt => pt.Usuario)
                .WithMany(t => t.UsuariosRoles)
                .HasForeignKey(p => p.UsuarioId),
            j =>
            {
                j.HasKey(pt => new {pt.UsuarioId, pt.RolId});
            });
    }
}