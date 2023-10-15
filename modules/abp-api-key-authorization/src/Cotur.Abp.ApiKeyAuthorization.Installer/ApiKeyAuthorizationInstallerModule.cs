﻿
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Cotur.Abp.ApiKeyAuthorization;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class ApiKeyAuthorizationInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<ApiKeyAuthorizationInstallerModule>();
        });
    }
}
