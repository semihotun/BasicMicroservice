using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Threading.Tasks;

namespace Insfrastructure.Context
{
    public class PageDbContextSeed
    {
        public async Task SeedAsync(PageDbContext context,
            ILogger<PageDbContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(PageDbContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                using (context)
                {
                    context.Database.Migrate();
                    await Task.CompletedTask;
                }
            });
        }
        private AsyncRetryPolicy CreatePolicy(ILogger<PageDbContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => System.TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning("Exception message");
                });
        }
    }
}
