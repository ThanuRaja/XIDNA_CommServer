using XICommServer.Localization;
using Volo.Abp.Application.Services;

namespace XICommServer;

/* Inherit your application services from this class.
 */
public abstract class XICommServerAppService : ApplicationService
{
    protected XICommServerAppService()
    {
        LocalizationResource = typeof(XICommServerResource);
    }
}
