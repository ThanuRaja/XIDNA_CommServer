using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace XICommServer.Data;

/* This is used if database provider does't define
 * IXICommServerDbSchemaMigrator implementation.
 */
public class NullXICommServerDbSchemaMigrator : IXICommServerDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
