using XICommServer.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using XICommServer.MailEventLogs;

namespace XICommServer.Web.Pages.MailEventLogs
{
    public class EditModalModel : XICommServerPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public MailEventLogUpdateViewModel MailEventLog { get; set; }

        private readonly IMailEventLogsAppService _mailEventLogsAppService;

        public EditModalModel(IMailEventLogsAppService mailEventLogsAppService)
        {
            _mailEventLogsAppService = mailEventLogsAppService;

            MailEventLog = new();
        }

        public async Task OnGetAsync()
        {
            var mailEventLog = await _mailEventLogsAppService.GetAsync(Id);
            MailEventLog = ObjectMapper.Map<MailEventLogDto, MailEventLogUpdateViewModel>(mailEventLog);

        }

        public async Task<NoContentResult> OnPostAsync()
        {

            await _mailEventLogsAppService.UpdateAsync(Id, ObjectMapper.Map<MailEventLogUpdateViewModel, MailEventLogUpdateDto>(MailEventLog));
            return NoContent();
        }
    }

    public class MailEventLogUpdateViewModel : MailEventLogUpdateDto
    {
    }
}