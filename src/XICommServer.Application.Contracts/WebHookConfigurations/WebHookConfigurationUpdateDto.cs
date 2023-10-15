using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfigurationUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string ApiSignatureVerificationKey { get; set; }
        public string? ClientWebhookUrl { get; set; }
        public bool ListenProcessed { get; set; }
        public bool ListenDeferred { get; set; }
        public bool ListenDelivered { get; set; }
        public bool ListenOpen { get; set; }
        public bool ListenClick { get; set; }
        public bool ListenBounce { get; set; }
        public bool ListenDropped { get; set; }
        public bool ListenSpamReport { get; set; }
        public bool ListenUnsubscribe { get; set; }
        public bool ListenGroupUnsubscribe { get; set; }
        public bool ListenGroupResubscribe { get; set; }
        public bool IsDefault { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}