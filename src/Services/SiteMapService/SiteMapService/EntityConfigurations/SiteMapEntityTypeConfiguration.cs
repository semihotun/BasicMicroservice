using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SiteMapService.AggregateModels;
using SiteMapService.Context;

namespace SiteMapService.EntityConfigurations
{
    public class SiteMapEntityTypeConfiguration : IEntityTypeConfiguration<SiteMap>
    {
        public void Configure(EntityTypeBuilder<SiteMap> builder)
        {
            builder.ToTable("SiteMap", SiteMapDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ci => ci.Id);

        }
    }

}
