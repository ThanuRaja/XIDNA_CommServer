using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using XICommServer.SendGridKeys;
using XICommServer.Shared;

namespace XICommServer.Web.Pages.SendGridKeys
{
    public class IndexModel : AbpPageModel
    {
        public string? NameFilter { get; set; }
        public string? APIKeyFilter { get; set; }
        public string? DisplayNameFilter { get; set; }
        public string? EmailAddressFilter { get; set; }
        [SelectItems(nameof(IsDefaultBoolFilterItems))]
        public string IsDefaultFilter { get; set; }

        public List<SelectListItem> IsDefaultBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly ISendGridKeysAppService _sendGridKeysAppService;

        public IndexModel(ISendGridKeysAppService sendGridKeysAppService)
        {
            _sendGridKeysAppService = sendGridKeysAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}