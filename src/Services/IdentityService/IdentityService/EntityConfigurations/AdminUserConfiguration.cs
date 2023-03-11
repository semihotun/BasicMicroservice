using IdentityService.Context;
using IdentityService.Context.ContextTable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.EntityConfigurations
{
    public class AdminUserConfiguration : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.ToTable("AdminUser", IdentityContext.DEFAULT_SCHEMA);

            builder.HasKey(ci => ci.Id);
        }
    }
}
