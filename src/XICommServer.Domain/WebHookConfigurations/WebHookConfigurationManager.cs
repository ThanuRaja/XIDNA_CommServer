using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace XICommServer.WebHookConfigurations
{
    public class WebHookConfigurationManager : DomainService
    {
        private readonly IWebHookConfigurationRepository _webHookConfigurationRepository;

        public WebHookConfigurationManager(IWebHookConfigurationRepository webHookConfigurationRepository)
        {
            _webHookConfigurationRepository = webHookConfigurationRepository;
        }

        public async Task<WebHookConfiguration> CreateAsync(
        string apiSignatureVerificationKey, string clientWebhookUrl, bool listenProcessed, bool listenDeferred, bool listenDelivered, bool listenOpen, bool listenClick, bool listenBounce, bool listenDropped, bool listenSpamReport, bool listenUnsubscribe, bool listenGroupUnsubscribe, bool listenGroupResubscribe, bool isDefault)
        {
            Check.NotNullOrWhiteSpace(apiSignatureVerificationKey, nameof(apiSignatureVerificationKey));

            var webHookConfiguration = new WebHookConfiguration(
             GuidGenerator.Create(),
             apiSignatureVerificationKey, clientWebhookUrl, listenProcessed, listenDeferred, listenDelivered, listenOpen, listenClick, listenBounce, listenDropped, listenSpamReport, listenUnsubscribe, listenGroupUnsubscribe, listenGroupResubscribe, isDefault
             );

            return await _webHookConfigurationRepository.InsertAsync(webHookConfiguration);
        }

        public async Task<WebHookConfiguration> UpdateAsync(
            Guid id,
            string apiSignatureVerificationKey, string clientWebhookUrl, bool listenProcessed, bool listenDeferred, bool listenDelivered, bool listenOpen, bool listenClick, bool listenBounce, bool listenDropped, bool listenSpamReport, bool listenUnsubscribe, bool listenGroupUnsubscribe, bool listenGroupResubscribe, bool isDefault, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(apiSignatureVerificationKey, nameof(apiSignatureVerificationKey));

            var webHookConfiguration = await _webHookConfigurationRepository.GetAsync(id);

            webHookConfiguration.ApiSignatureVerificationKey = apiSignatureVerificationKey;
            webHookConfiguration.ClientWebhookUrl = clientWebhookUrl;
            webHookConfiguration.ListenProcessed = listenProcessed;
            webHookConfiguration.ListenDeferred = listenDeferred;
            webHookConfiguration.ListenDelivered = listenDelivered;
            webHookConfiguration.ListenOpen = listenOpen;
            webHookConfiguration.ListenClick = listenClick;
            webHookConfiguration.ListenBounce = listenBounce;
            webHookConfiguration.ListenDropped = listenDropped;
            webHookConfiguration.ListenSpamReport = listenSpamReport;
            webHookConfiguration.ListenUnsubscribe = listenUnsubscribe;
            webHookConfiguration.ListenGroupUnsubscribe = listenGroupUnsubscribe;
            webHookConfiguration.ListenGroupResubscribe = listenGroupResubscribe;
            webHookConfiguration.IsDefault = isDefault;

            webHookConfiguration.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _webHookConfigurationRepository.UpdateAsync(webHookConfiguration);
        }

    }
}