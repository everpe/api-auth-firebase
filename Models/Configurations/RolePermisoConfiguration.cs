using Firebase.Api.Models.Domain;
using Firebase.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firebase.Api.Models.Configurations;

public class RolePermisoConfiguration : IEntityTypeConfiguration<RolePermiso>
{
    public void Configure(EntityTypeBuilder<RolePermiso> builder)
    {
        builder.HasKey(x => new { x.RoleId, x.PermisoId });
        builder.HasData(
            Crear(Role.Cliente, PermisoEnum.ReadUsuario),
            Crear(Role.Cliente, PermisoEnum.WriteUsuario)
        );
    }

    private static RolePermiso Crear(Role role, PermisoEnum permiso)
    {
        return new RolePermiso
        {
            RoleId = role.Id,
            PermisoId = (int)permiso
        };
    }

}