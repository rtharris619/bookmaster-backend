using Bookmaster.Modules.Users.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Users.Infrastructure.Users;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("permissions");

        builder.HasKey(p => p.Code);

        builder.Property(p => p.Code)
            .HasMaxLength(100);

        builder.HasData(
            Permission.GetUser, 
            Permission.ModifyUser, 
            Permission.SearchGoogleBooks);

        builder
            .HasMany<Role>()
            .WithMany()
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("role_permissions");
                joinBuilder.HasData(
                    CreateRolePermission(Role.Member, Permission.GetUser),
                    CreateRolePermission(Role.Member, Permission.ModifyUser),
                    CreateRolePermission(Role.Member, Permission.SearchGoogleBooks),

                    CreateRolePermission(Role.Admin, Permission.GetUser),
                    CreateRolePermission(Role.Admin, Permission.ModifyUser),
                    CreateRolePermission(Role.Admin, Permission.SearchGoogleBooks),

                    CreateRolePermission(Role.SuperAdmin, Permission.GetUser),
                    CreateRolePermission(Role.SuperAdmin, Permission.ModifyUser),
                    CreateRolePermission(Role.SuperAdmin, Permission.SearchGoogleBooks)
                );
            });
    }

    private static object CreateRolePermission(Role role, Permission permission)
    {
        return new
        {
            RoleName = role.Name,
            PermissionCode = permission.Code,
        };
    }
}
