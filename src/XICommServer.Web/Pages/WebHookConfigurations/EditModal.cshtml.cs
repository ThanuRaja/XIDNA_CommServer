using XICommServer.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using XICommServer.WebHookConfigurations;

namespace XICommServer.Web.Pages.WebHookConfigurations
{
    public class EditModalModel : XICommServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public WebHookConfigurationUpdateViewModel WebHookConfiguration { get; set; }

        private readonly IWebHookConfigurationsAppService _webHookConfigurationsAppService;

        public EditModalModel(IWebHookConfigurationsAppService webHookConfigurationsAppService)
        {
            _webHookConfigurationsAppService = webHookConfigurationsAppService;

            WebHookConfiguration = new();
        }

        public async Task OnGetAsync()
        {
            var webHookConfiguration = await _webHookConfigurationsAppService.GetAsync(Id);
            WebHookConfiguration = ObjectMapper.Map<WebHookConfigurationDto, WebHookConfigurationUpdateViewModel>(webHookConfiguration);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _webHookConfigurationsAppService.UpdateAsync(Id, ObjectMapper.Map<WebHookConfigurationUpdateViewModel, WebHookConfigurationUpdateDto>(WebHookConfiguration));
            return NoContent();
        }
    }

    public class WebHookConfigurationUpdateViewModel : WebHookConfigurationUpdateDto
    {
    }
}