using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using XICommServer.EntityFrameworkCore;

namespace XICommServer.WebHookConfigurations
{
    public class EfCoreWebHookConfigurationRepository : EfCoreRepository<XICommServerDbContext, WebHookConfiguration, Guid>, IWebHookConfigurationRepository
    {
        public EfCoreWebHookConfigurationRepository(IDbContextProvider<XICommServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<WebHookConfiguration>> GetListAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, apiSignatureVerificationKey, clientWebhookUrl, listenProcessed, listenDeferred, listenDelivered, listenOpen, listenClick, listenBounce, listenDropped, listenSpamReport, listenUnsubscribe, listenGroupUnsubscribe, listenGroupResubscribe, isDefault);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WebHookConfigurationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, apiSignatureVerificationKey, clientWebhookUrl, listenProcessed, listenDeferred, listenDelivered, listenOpen, listenClick, listenBounce, listenDropped, listenSpamReport, listenUnsubscribe, listenGroupUnsubscribe, listenGroupResubscribe, isDefault);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<WebHookConfiguration> ApplyFilter(
            IQueryable<WebHookConfiguration> query,
            string filterText,
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
            bool? isDefault = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ApiSignatureVerificationKey.Contains(filterText) || e.ClientWebhookUrl.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(apiSignatureVerificationKey), e => e.ApiSignatureVerificationKey.Contains(apiSignatureVerificationKey))
                    .WhereIf(!string.IsNullOrWhiteSpace(clientWebhookUrl), e => e.ClientWebhookUrl.Contains(clientWebhookUrl))
                    .WhereIf(listenProcessed.HasValue, e => e.ListenProcessed == listenProcessed)
                    .WhereIf(listenDeferred.HasValue, e => e.ListenDeferred == listenDeferred)
                    .WhereIf(listenDelivered.HasValue, e => e.ListenDelivered == listenDelivered)
                    .WhereIf(listenOpen.HasValue, e => e.ListenOpen == listenOpen)
                    .WhereIf(listenClick.HasValue, e => e.ListenClick == listenClick)
                    .WhereIf(listenBounce.HasValue, e => e.ListenBounce == listenBounce)
                    .WhereIf(listenDropped.HasValue, e => e.ListenDropped == listenDropped)
                    .WhereIf(listenSpamReport.HasValue, e => e.ListenSpamReport == listenSpamReport)
                    .WhereIf(listenUnsubscribe.HasValue, e => e.ListenUnsubscribe == listenUnsubscribe)
                    .WhereIf(listenGroupUnsubscribe.HasValue, e => e.ListenGroupUnsubscribe == listenGroupUnsubscribe)
                    .WhereIf(listenGroupResubscribe.HasValue, e => e.ListenGroupResubscribe == listenGroupResubscribe)
                    .WhereIf(isDefault.HasValue, e => e.IsDefault == isDefault);
        }
    }
}