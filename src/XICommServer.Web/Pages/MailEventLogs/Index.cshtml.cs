using XICommServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using XICommServer.MailEventLogs;
using XICommServer.Shared;

namespace XICommServer.Web.Pages.MailEventLogs
{
    public class IndexModel : AbpPageModel
    {
        public DateTime? TimestampFilterMin { get; set; }

        public DateTime? TimestampFilterMax { get; set; }
        public string? SmtpIdFilter { get; set; }
        public EventType? EventTypeFilter { get; set; }
        public string? CategoryFilter { get; set; }
        public string? SendGridEventIdFilter { get; set; }
        public string? SendGridMessageIdFilter { get; set; }
        public string? TLSFilter { get; set; }
        public string? MarketingCampainIdFilter { get; set; }
        public string? MarketingCampainNameFilter { get; set; }
        [SelectItems(nameof(IsLogSyncedBoolFilterItems))]
        public string IsLogSyncedFilter { get; set; }

        public List<SelectListItem> IsLogSyncedBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IMailEventLogsAppService _mailEventLogsAppService;

        public IndexModel(IMailEventLogsAppService mailEventLogsAppService)
        {
            _mailEventLogsAppService = mailEventLogsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}