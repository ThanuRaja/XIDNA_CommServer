using XICommServer.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace XICommServer.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(XICommServerEntityFrameworkCoreModule),
    typeof(XICommServerApplicationContractsModule)
)]
public class XICommServerDbMigratorModule : AbpModule
{

}
