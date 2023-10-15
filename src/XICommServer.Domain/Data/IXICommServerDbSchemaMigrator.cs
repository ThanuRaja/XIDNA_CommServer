using System.Threading.Tasks;

namespace XICommServer.Data;

public interface IXICommServerDbSchemaMigrator
{
    Task MigrateAsync();
}
