using Volo.Abp.Modularity;

namespace XICommServer;

[DependsOn(
    typeof(XICommServerApplicationModule),
    typeof(XICommServerDomainTestModule)
    )]
public class XICommServerApplicationTestModule : AbpModule
{

}
