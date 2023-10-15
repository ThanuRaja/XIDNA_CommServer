using XICommServer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace XICommServer;

[DependsOn(
    typeof(XICommServerEntityFrameworkCoreTestModule)
    )]
public class XICommServerDomainTestModule : AbpModule
{

}
