using Microsoft.EntityFrameworkCore;
using SiteMapService.AggregateModels;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SiteMapService.SeedWork
{
    public interface IRepository : IDisposable
    {
        DbSet<SiteMap> SiteMap { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
