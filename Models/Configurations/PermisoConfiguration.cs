using Firebase.Api.Models.Domain;
using Firebase.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firebase.Api.Models.Configurations;

public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
    public void Configure(EntityTypeBuilder<Permiso> builder)
    {
        builder.ToTable("Permisos");

        builder.HasKey(p=> p.Id);

        IEnumerable<Permiso> permisos = Enum.GetValues<PermisoEnum>()
                        .Select(p => new Permiso {
                            Id = (int)p,
                            Nombre = p.ToString()
                        });

        builder.HasData(permisos);

    }
}