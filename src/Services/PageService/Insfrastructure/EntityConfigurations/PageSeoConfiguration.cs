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
    public class PageSeoConfiguration : IEntityTypeConfiguration<PageSeo>
    {
        public void Configure(EntityTypeBuilder<PageSeo> builder)
        {
            builder.ToTable("PageSeo", PageDbContext.DEFAULT_SCHEMA);

            builder.HasKey(ci => ci.Id);


        }
    }
}
