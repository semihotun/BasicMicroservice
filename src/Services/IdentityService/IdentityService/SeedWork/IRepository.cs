using IdentityService.Context.ContextTable;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityService.SeedWork
{
    public interface IRepository : IDisposable
    {
        DbSet<AdminUser> AdminUser { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

}
