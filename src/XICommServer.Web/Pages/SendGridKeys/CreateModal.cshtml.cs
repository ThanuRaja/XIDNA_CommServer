using XICommServer.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XICommServer.SendGridKeys;

namespace XICommServer.Web.Pages.SendGridKeys
{
    public class CreateModalModel : XICommServerPageModel
    {
        [BindProperty]
        public SendGridKeyCreateViewModel SendGridKey { get; set; }

        private readonly ISendGridKeysAppService _sendGridKeysAppService;

        public CreateModalModel(ISendGridKeysAppService sendGridKeysAppService)
        {
            _sendGridKeysAppService = sendGridKeysAppService;

            SendGridKey = new();
        }

        public async Task OnGetAsync()
        {
            SendGridKey = new SendGridKeyCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _sendGridKeysAppService.CreateAsync(ObjectMapper.Map<SendGridKeyCreateViewModel, SendGridKeyCreateDto>(SendGridKey));
            return NoContent();
        }
    }

    public class SendGridKeyCreateViewModel : SendGridKeyCreateDto
    {
    }
}