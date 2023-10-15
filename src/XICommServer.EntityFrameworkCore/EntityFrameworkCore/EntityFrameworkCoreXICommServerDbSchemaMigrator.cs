using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using XICommServer.Data;
using Volo.Abp.DependencyInjection;

namespace XICommServer.EntityFrameworkCore;

public class EntityFrameworkCoreXICommServerDbSchemaMigrator
    : IXICommServerDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreXICommServerDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the XICommServerDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<XICommServerDbContext>()
            .Database
            .MigrateAsync();
    }
}
