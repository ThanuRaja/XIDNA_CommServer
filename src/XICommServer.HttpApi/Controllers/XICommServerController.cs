using XICommServer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace XICommServer.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class XICommServerController : AbpControllerBase
{
    protected XICommServerController()
    {
        LocalizationResource = typeof(XICommServerResource);
    }
}
