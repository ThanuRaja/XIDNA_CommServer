using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace XICommServer.MailEvents
{
    public class MailEventDto : EntityDto<Guid>, IHasConcurrencyStamp
    {
        public string? SGMessageId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSuccess { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}