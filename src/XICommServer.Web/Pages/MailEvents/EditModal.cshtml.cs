using XICommServer.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using XICommServer.MailEvents;

namespace XICommServer.Web.Pages.MailEvents
{
    public class EditModalModel : XICommServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public MailEventUpdateViewModel MailEvent { get; set; }

        private readonly IMailEventsAppService _mailEventsAppService;

        public EditModalModel(IMailEventsAppService mailEventsAppService)
        {
            _mailEventsAppService = mailEventsAppService;

            MailEvent = new();
        }

        public async Task OnGetAsync()
        {
            var mailEvent = await _mailEventsAppService.GetAsync(Id);
            MailEvent = ObjectMapper.Map<MailEventDto, MailEventUpdateViewModel>(mailEvent);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _mailEventsAppService.UpdateAsync(Id, ObjectMapper.Map<MailEventUpdateViewModel, MailEventUpdateDto>(MailEvent));
            return NoContent();
        }
    }

    public class MailEventUpdateViewModel : MailEventUpdateDto
    {
    }
}