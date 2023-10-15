using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using XICommServer.WebHookConfigurations;
using XICommServer.Shared;

namespace XICommServer.Web.Pages.WebHookConfigurations
{
    public class IndexModel : AbpPageModel
    {
        public string? ApiSignatureVerificationKeyFilter { get; set; }
        public string? ClientWebhookUrlFilter { get; set; }
        [SelectItems(nameof(ListenProcessedBoolFilterItems))]
        public string ListenProcessedFilter { get; set; }

        public List<SelectListItem> ListenProcessedBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenDeferredBoolFilterItems))]
        public string ListenDeferredFilter { get; set; }

        public List<SelectListItem> ListenDeferredBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenDeliveredBoolFilterItems))]
        public string ListenDeliveredFilter { get; set; }

        public List<SelectListItem> ListenDeliveredBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenOpenBoolFilterItems))]
        public string ListenOpenFilter { get; set; }

        public List<SelectListItem> ListenOpenBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenClickBoolFilterItems))]
        public string ListenClickFilter { get; set; }

        public List<SelectListItem> ListenClickBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenBounceBoolFilterItems))]
        public string ListenBounceFilter { get; set; }

        public List<SelectListItem> ListenBounceBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenDroppedBoolFilterItems))]
        public string ListenDroppedFilter { get; set; }

        public List<SelectListItem> ListenDroppedBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenSpamReportBoolFilterItems))]
        public string ListenSpamReportFilter { get; set; }

        public List<SelectListItem> ListenSpamReportBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenUnsubscribeBoolFilterItems))]
        public string ListenUnsubscribeFilter { get; set; }

        public List<SelectListItem> ListenUnsubscribeBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenGroupUnsubscribeBoolFilterItems))]
        public string ListenGroupUnsubscribeFilter { get; set; }

        public List<SelectListItem> ListenGroupUnsubscribeBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(ListenGroupResubscribeBoolFilterItems))]
        public string ListenGroupResubscribeFilter { get; set; }

        public List<SelectListItem> ListenGroupResubscribeBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };
        [SelectItems(nameof(IsDefaultBoolFilterItems))]
        public string IsDefaultFilter { get; set; }

        public List<SelectListItem> IsDefaultBoolFilterItems { get; set; } =
            new List<SelectListItem>
            {
                new SelectListItem("", ""),
                new SelectListItem("Yes", "true"),
                new SelectListItem("No", "false"),
            };

        private readonly IWebHookConfigurationsAppService _webHookConfigurationsAppService;

        public IndexModel(IWebHookConfigurationsAppService webHookConfigurationsAppService)
        {
            _webHookConfigurationsAppService = webHookConfigurationsAppService;
        }

        public async Task OnGetAsync()
        {

            await Task.CompletedTask;
        }
    }
}