using Domain.AggregateModels.PageAggregate;
using Insfrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insfrastructure.EntityConfigurations
{
    public class PageEntityTypeConfiguration:IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.ToTable("Page", PageDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.PageSeo)
             .WithMany()
             .HasForeignKey(ci => ci.PageSeoId);
        }
    }

}
