using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using XICommServer.MailEvents;
using XICommServer.Shared;

namespace XICommServer.Web.Pages.MailEvents
{
    public class IndexModel : AbpPageModel
    {
        public string? SGMessageIdFilter { get; set; }
        public DateTime? CreatedAtFilterMin { get; set; }

        public DateTime? CreatedAtFilterMax { get; set; }
        [SelectItems(nameof(IsSuccessBoolFilterItems))]
        public string IsSuccessFilter { get; set; }

        public List<SelectListItem> IsSuccessBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IMailEventsAppService _mailEventsAppService;

        public IndexModel(IMailEventsAppService mailEventsAppService)
        {
            _mailEventsAppService = mailEventsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}