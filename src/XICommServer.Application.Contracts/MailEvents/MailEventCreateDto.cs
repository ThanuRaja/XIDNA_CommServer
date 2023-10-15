using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace XICommServer.MailEvents
{
    public class MailEventCreateDto
    {
        public string? SGMessageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSuccess { get; set; }
    }
}