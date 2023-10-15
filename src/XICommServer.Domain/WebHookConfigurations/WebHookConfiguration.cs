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

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfiguration : Entity<Guid>, IHasConcurrencyStamp
    {
        [NotNull]
        public virtual string ApiSignatureVerificationKey { get; set; }

        [CanBeNull]
        public virtual string? ClientWebhookUrl { get; set; }

        public virtual bool ListenProcessed { get; set; }

        public virtual bool ListenDeferred { get; set; }

        public virtual bool ListenDelivered { get; set; }

        public virtual bool ListenOpen { get; set; }

        public virtual bool ListenClick { get; set; }

        public virtual bool ListenBounce { get; set; }

        public virtual bool ListenDropped { get; set; }

        public virtual bool ListenSpamReport { get; set; }

        public virtual bool ListenUnsubscribe { get; set; }

        public virtual bool ListenGroupUnsubscribe { get; set; }

        public virtual bool ListenGroupResubscribe { get; set; }

        public virtual bool IsDefault { get; set; }

        public string ConcurrencyStamp { get; set; }

        public WebHookConfiguration()
        {

        }

        public WebHookConfiguration(Guid id, string apiSignatureVerificationKey, string clientWebhookUrl, bool listenProcessed, bool listenDeferred, bool listenDelivered, bool listenOpen, bool listenClick, bool listenBounce, bool listenDropped, bool listenSpamReport, bool listenUnsubscribe, bool listenGroupUnsubscribe, bool listenGroupResubscribe, bool isDefault)
        {
            ConcurrencyStamp = Guid.NewGuid().ToString("N");
            Id = id;
            Check.NotNull(apiSignatureVerificationKey, nameof(apiSignatureVerificationKey));
            ApiSignatureVerificationKey = apiSignatureVerificationKey;
            ClientWebhookUrl = clientWebhookUrl;
            ListenProcessed = listenProcessed;
            ListenDeferred = listenDeferred;
            ListenDelivered = listenDelivered;
            ListenOpen = listenOpen;
            ListenClick = listenClick;
            ListenBounce = listenBounce;
            ListenDropped = listenDropped;
            ListenSpamReport = listenSpamReport;
            ListenUnsubscribe = listenUnsubscribe;
            ListenGroupUnsubscribe = listenGroupUnsubscribe;
            ListenGroupResubscribe = listenGroupResubscribe;
            IsDefault = isDefault;
        }

    }
}