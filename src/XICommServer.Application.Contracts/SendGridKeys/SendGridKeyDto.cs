using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace XICommServer.SendGridKeys
{
    public class SendGridKeyDto : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
    {
        public string Name { get; set; }
        public string APIKey { get; set; }
        public string? DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDefault { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}