using XICommServer;
using System;

namespace XICommServer.MailEventLogs
{
    public class MailEventLogExcelDto
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
    }
}