using XICommServer;
using Volo.Abp.Application.Dtos;
using System;

namespace XICommServer.MailEventLogs
{
    public class MailEventLogExcelDownloadDto
    {
        public string DownloadToken { get; set; }

        public string? FilterText { get; set; }

        public DateTime? TimestampMin { get; set; }
        public DateTime? TimestampMax { get; set; }
        public string? SmtpId { get; set; }
        public EventType? EventType { get; set; }
        public string? Category { get; set; }
        public string? SendGridEventId { get; set; }
        public string? SendGridMessageId { get; set; }
        public string? TLS { get; set; }
        public string? MarketingCampainId { get; set; }
        public string? MarketingCampainName { get; set; }
        public bool? IsLogSynced { get; set; }

        public MailEventLogExcelDownloadDto()
        {

        }
    }
}