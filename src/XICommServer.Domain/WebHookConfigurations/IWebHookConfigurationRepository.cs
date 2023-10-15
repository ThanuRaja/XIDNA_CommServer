using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace XICommServer.WebHookConfigurations
{
    public interface IWebHookConfigurationRepository : IRepository<WebHookConfiguration, Guid>
    {
        Task<List<WebHookConfiguration>> GetListAsync(
            string filterText = null,
            string apiSignatureVerificationKey = null,
            string clientWebhookUrl = null,
            bool? listenProcessed = null,
            bool? listenDeferred = null,
            bool? listenDelivered = null,
            bool? listenOpen = null,
            bool? listenClick = null,
            bool? listenBounce = null,
            bool? listenDropped = null,
            bool? listenSpamReport = null,
            bool? listenUnsubscribe = null,
            bool? listenGroupUnsubscribe = null,
            bool? listenGroupResubscribe = null,
            bool? isDefault = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string apiSignatureVerificationKey = null,
            string clientWebhookUrl = null,
            bool? listenProcessed = null,
            bool? listenDeferred = null,
            bool? listenDelivered = null,
            bool? listenOpen = null,
            bool? listenClick = null,
            bool? listenBounce = null,
            bool? listenDropped = null,
            bool? listenSpamReport = null,
            bool? listenUnsubscribe = null,
            bool? listenGroupUnsubscribe = null,
            bool? listenGroupResubscribe = null,
            bool? isDefault = null,
            CancellationToken cancellationToken = default);
    }
}