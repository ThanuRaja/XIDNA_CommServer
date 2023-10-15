using XICommServer.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XICommServer.MailEventLogs;

namespace XICommServer.Web.Pages.MailEventLogs
{
    public class CreateModalModel : XICommServerPageModel
    {
        [BindProperty]
        public MailEventLogCreateViewModel MailEventLog { get; set; }

        private readonly IMailEventLogsAppService _mailEventLogsAppService;

        public CreateModalModel(IMailEventLogsAppService mailEventLogsAppService)
        {
            _mailEventLogsAppService = mailEventLogsAppService;

            MailEventLog = new();
        }

        public async Task OnGetAsync()
        {
            MailEventLog = new MailEventLogCreateViewModel();

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            await _mailEventLogsAppService.CreateAsync(ObjectMapper.Map<MailEventLogCreateViewModel, MailEventLogCreateDto>(MailEventLog));
            return NoContent();
        }
    }

    public class MailEventLogCreateViewModel : MailEventLogCreateDto
    {
    }
}