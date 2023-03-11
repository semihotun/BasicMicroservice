using Domain.AggregateModels.PageAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface IRepository : IDisposable
    {
        DbSet<Page> Page { get; set; }
        DbSet<PageSeo> PageSeo { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
