using XICommServer.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XICommServer.WebHookConfigurations;

namespace XICommServer.Web.Pages.WebHookConfigurations
{
    public class CreateModalModel : XICommServerPageModel
    {
        [BindProperty]
        public WebHookConfigurationCreateViewModel WebHookConfiguration { get; set; }

        private readonly IWebHookConfigurationsAppService _webHookConfigurationsAppService;

        public CreateModalModel(IWebHookConfigurationsAppService webHookConfigurationsAppService)
        {
            _webHookConfigurationsAppService = webHookConfigurationsAppService;

            WebHookConfiguration = new();
        }

        public async Task OnGetAsync()
        {
            WebHookConfiguration = new WebHookConfigurationCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _webHookConfigurationsAppService.CreateAsync(ObjectMapper.Map<WebHookConfigurationCreateViewModel, WebHookConfigurationCreateDto>(WebHookConfiguration));
            return NoContent();
        }
    }

    public class WebHookConfigurationCreateViewModel : WebHookConfigurationCreateDto
    {
    }
}