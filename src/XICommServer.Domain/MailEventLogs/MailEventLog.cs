using XICommServer;
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

namespace XICommServer.MailEventLogs
{
    public class MailEventLog : Entity<Guid>, IMultiTenant, IHasConcurrencyStamp
    {
        public virtual Guid? TenantId { get; set; }

        public virtual DateTime Timestamp { get; set; }

        [CanBeNull]
        public virtual string? SmtpId { get; set; }

        public virtual EventType EventType { get; set; }

        [CanBeNull]
        public virtual string? Category { get; set; }

        [CanBeNull]
        public virtual string? SendGridEventId { get; set; }

        [CanBeNull]
        public virtual string? SendGridMessageId { get; set; }

        [CanBeNull]
        public virtual string? TLS { get; set; }

        [CanBeNull]
        public virtual string? MarketingCampainId { get; set; }

        [CanBeNull]
        public virtual string? MarketingCampainName { get; set; }

        public virtual bool IsLogSynced { get; set; }

        public string ConcurrencyStamp { get; set; }

        public MailEventLog()
        {

        }

        public MailEventLog(Guid id, DateTime timestamp, string smtpId, EventType eventType, string category, string sendGridEventId, string sendGridMessageId, string tLS, string marketingCampainId, string marketingCampainName, bool isLogSynced)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Timestamp = timestamp;
            SmtpId = smtpId;
            EventType = eventType;
            Category = category;
            SendGridEventId = sendGridEventId;
            SendGridMessageId = sendGridMessageId;
            TLS = tLS;
            MarketingCampainId = marketingCampainId;
            MarketingCampainName = marketingCampainName;
            IsLogSynced = isLogSynced;
        }

    }
}