using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

using Volo.Abp;

namespace XICommServer.MailEvents
{
    public class MailEvent : Entity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public virtual Guid? TenantId { get; set; }

        [CanBeNull]
        public virtual string? SGMessageId { get; set; }

        public virtual DateTime? CreatedAt { get; set; }

        public virtual bool IsSuccess { get; set; }

        public string ConcurrencyStamp { get; set; }

        public MailEvent()
        {

        }

        public MailEvent(Guid id, string sGMessageId, bool isSuccess, DateTime? createdAt = null)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            SGMessageId = sGMessageId;
            IsSuccess = isSuccess;
            CreatedAt = createdAt;
        }

    }
}