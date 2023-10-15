using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace XICommServer.Web;

[Dependency(ReplaceServices = true)]
public class XICommServerBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "XICommServer";
}
