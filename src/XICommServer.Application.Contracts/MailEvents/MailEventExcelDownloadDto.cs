using Volo.Abp.Application.Dtos;
using System;

namespace XICommServer.MailEvents
{
    public class MailEventExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public string? SGMessageId { get; set; }
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
        public bool? IsSuccess { get; set; }

        public MailEventExcelDownloadDto()
        {

        }
    }
}