using System;

namespace XICommServer.MailEvents
{
    public class MailEventExcelDto
    {
        public string? SGMessageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSuccess { get; set; }
    }
}