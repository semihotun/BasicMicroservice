using MediatR;
using Microsoft.EntityFrameworkCore;
using SiteMapService.AggregateModels;
using SiteMapService.Extension;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using SiteMapService.SeedWork;
namespace SiteMapService.Context
{
    public  class SiteMapDbContext : DbContext , IRepository
    {
        public const string DEFAULT_SCHEMA = "SiteMapDbContext";

        private readonly IMediator mediator;


        public SiteMapDbContext() : base()
        {

        }

        public SiteMapDbContext(DbContextOptions<SiteMapDbContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }

        public DbSet<SiteMap> SiteMap { get; set; }

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
