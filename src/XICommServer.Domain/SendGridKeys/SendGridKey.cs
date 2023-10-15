using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace XICommServer.SendGridKeys
{
    public class SendGridKey : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; set; }

        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string APIKey { get; set; }

        [CanBeNull]
        public virtual string? DisplayName { get; set; }

        [NotNull]
        public virtual string EmailAddress { get; set; }

        public virtual bool IsDefault { get; set; }

        public SendGridKey()
        {

        }

        public SendGridKey(Guid id, string name, string aPIKey, string displayName, string emailAddress, bool isDefault)
        {

            Id = id;
            Check.NotNull(name, nameof(name));
            Check.NotNull(aPIKey, nameof(aPIKey));
            Check.NotNull(emailAddress, nameof(emailAddress));
            Name = name;
            APIKey = aPIKey;
            DisplayName = displayName;
            EmailAddress = emailAddress;
            IsDefault = isDefault;
        }

    }
}