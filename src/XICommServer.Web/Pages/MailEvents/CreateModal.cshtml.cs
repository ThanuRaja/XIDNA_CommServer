using XICommServer.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XICommServer.MailEvents;

namespace XICommServer.Web.Pages.MailEvents
{
    public class CreateModalModel : XICommServerPageModel
    {
        [BindProperty]
        public MailEventCreateViewModel MailEvent { get; set; }

        private readonly IMailEventsAppService _mailEventsAppService;

        public CreateModalModel(IMailEventsAppService mailEventsAppService)
        {
            _mailEventsAppService = mailEventsAppService;

            MailEvent = new();
        }

        public async Task OnGetAsync()
        {
            MailEvent = new MailEventCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _mailEventsAppService.CreateAsync(ObjectMapper.Map<MailEventCreateViewModel, MailEventCreateDto>(MailEvent));
            return NoContent();
        }
    }

    public class MailEventCreateViewModel : MailEventCreateDto
    {
    }
}