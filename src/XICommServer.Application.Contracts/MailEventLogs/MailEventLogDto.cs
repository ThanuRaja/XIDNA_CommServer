using XICommServer;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace XICommServer.MailEventLogs
{
    public class MailEventLogDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public DateTime Timestamp { get; set; }
        public string? SmtpId { get; set; }
        public EventType EventType { get; set; }
        public string? Category { get; set; }
        public string? SendGridEventId { get; set; }
        public string? SendGridMessageId { get; set; }
        public string? TLS { get; set; }
        public string? MarketingCampainId { get; set; }
        public string? MarketingCampainName { get; set; }
        public bool IsLogSynced { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}