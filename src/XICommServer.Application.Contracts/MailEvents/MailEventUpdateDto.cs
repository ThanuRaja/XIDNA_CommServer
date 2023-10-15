using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace XICommServer.MailEvents
{
    public class MailEventUpdateDto : IHasConcurrencyStamp
    {
        public string? SGMessageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSuccess { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}