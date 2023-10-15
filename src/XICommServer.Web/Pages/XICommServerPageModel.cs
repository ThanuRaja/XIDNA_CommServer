using XICommServer.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace XICommServer.Web.Pages;

public abstract class XICommServerPageModel : AbpPageModel
{
    protected XICommServerPageModel()
    {
        LocalizationResourceType = typeof(XICommServerResource);
    }
}
