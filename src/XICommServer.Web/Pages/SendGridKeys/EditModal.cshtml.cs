using XICommServer.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using XICommServer.SendGridKeys;

namespace XICommServer.Web.Pages.SendGridKeys
{
    public class EditModalModel : XICommServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public SendGridKeyUpdateViewModel SendGridKey { get; set; }

        private readonly ISendGridKeysAppService _sendGridKeysAppService;

        public EditModalModel(ISendGridKeysAppService sendGridKeysAppService)
        {
            _sendGridKeysAppService = sendGridKeysAppService;

            SendGridKey = new();
        }

        public async Task OnGetAsync()
        {
            var sendGridKey = await _sendGridKeysAppService.GetAsync(Id);
            SendGridKey = ObjectMapper.Map<SendGridKeyDto, SendGridKeyUpdateViewModel>(sendGridKey);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _sendGridKeysAppService.UpdateAsync(Id, ObjectMapper.Map<SendGridKeyUpdateViewModel, SendGridKeyUpdateDto>(SendGridKey));
            return NoContent();
        }
    }

    public class SendGridKeyUpdateViewModel : SendGridKeyUpdateDto
    {
    }
}