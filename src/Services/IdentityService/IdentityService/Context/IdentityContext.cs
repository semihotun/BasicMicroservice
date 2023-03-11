using IdentityService.Context.ContextTable;
using IdentityService.Extension;
using IdentityService.SeedWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.Context
{
    public class IdentityContext : DbContext, IRepository
    {
        public const string DEFAULT_SCHEMA = "IdentityContext";
        private readonly IMediator mediator;
        public IdentityContext() : base() 
        {

        }

        public IdentityContext(DbContextOptions<IdentityContext> options, IMediator mediator) : base(options)
        {
            this.mediator = mediator;
        }
        public DbSet<AdminUser> AdminUser { get; set; }
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
