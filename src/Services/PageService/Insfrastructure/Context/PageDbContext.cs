using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using Domain.AggregateModels.PageAggregate;
using Insfrastructure.Extensions;
using Domain.SeedWork;
using System.Reflection;

namespace Insfrastructure.Context
{
    public  class PageDbContext : DbContext , IRepository
    {
        public const string DEFAULT_SCHEMA = "PageDbContext";

        private readonly IMediator mediator;

        public DbSet<Page> Page { get; set; }
        public DbSet<PageSeo> PageSeo { get; set; }

        public PageDbContext() : base()
        {

        }

        public PageDbContext(DbContextOptions<PageDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }
        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);

            return 1;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assm = Assembly.GetExecutingAssembly();
            modelBuilder.ApplyConfigurationsFromAssembly(assm);
        }
    }
}
