using XICommServer;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace XICommServer.MailEventLogs
{
    public class MailEventLogCreateDto
    {
        public DateTime Timestamp { get; set; }
        public string? SmtpId { get; set; }
        public EventType EventType { get; set; } = ((EventType[])Enum.GetValues(typeof(EventType)))[0];
        public string? Category { get; set; }
        public string? SendGridEventId { get; set; }
        public string? SendGridMessageId { get; set; }
        public string? TLS { get; set; }
        public string? MarketingCampainId { get; set; }
        public string? MarketingCampainName { get; set; }
        public bool IsLogSynced { get; set; }
    }
}