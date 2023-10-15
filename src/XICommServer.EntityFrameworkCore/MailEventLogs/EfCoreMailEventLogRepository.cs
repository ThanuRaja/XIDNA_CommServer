using XICommServer;
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

namespace XICommServer.MailEventLogs
{
    public class EfCoreMailEventLogRepository : EfCoreRepository<XICommServerDbContext, MailEventLog, Guid>, IMailEventLogRepository
    {
        public EfCoreMailEventLogRepository(IDbContextProvider<XICommServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<MailEventLog>> GetListAsync(
            string filterText = null,
            DateTime? timestampMin = null,
            DateTime? timestampMax = null,
            string smtpId = null,
            EventType? eventType = null,
            string category = null,
            string sendGridEventId = null,
            string sendGridMessageId = null,
            string tLS = null,
            string marketingCampainId = null,
            string marketingCampainName = null,
            bool? isLogSynced = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, timestampMin, timestampMax, smtpId, eventType, category, sendGridEventId, sendGridMessageId, tLS, marketingCampainId, marketingCampainName, isLogSynced);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MailEventLogConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            DateTime? timestampMin = null,
            DateTime? timestampMax = null,
            string smtpId = null,
            EventType? eventType = null,
            string category = null,
            string sendGridEventId = null,
            string sendGridMessageId = null,
            string tLS = null,
            string marketingCampainId = null,
            string marketingCampainName = null,
            bool? isLogSynced = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, timestampMin, timestampMax, smtpId, eventType, category, sendGridEventId, sendGridMessageId, tLS, marketingCampainId, marketingCampainName, isLogSynced);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MailEventLog> ApplyFilter(
            IQueryable<MailEventLog> query,
            string filterText,
            DateTime? timestampMin = null,
            DateTime? timestampMax = null,
            string smtpId = null,
            EventType? eventType = null,
            string category = null,
            string sendGridEventId = null,
            string sendGridMessageId = null,
            string tLS = null,
            string marketingCampainId = null,
            string marketingCampainName = null,
            bool? isLogSynced = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SmtpId.Contains(filterText) || e.Category.Contains(filterText) || e.SendGridEventId.Contains(filterText) || e.SendGridMessageId.Contains(filterText) || e.TLS.Contains(filterText) || e.MarketingCampainId.Contains(filterText) || e.MarketingCampainName.Contains(filterText))
                    .WhereIf(timestampMin.HasValue, e => e.Timestamp >= timestampMin.Value)
                    .WhereIf(timestampMax.HasValue, e => e.Timestamp <= timestampMax.Value)
                    .WhereIf(!string.IsNullOrWhiteSpace(smtpId), e => e.SmtpId.Contains(smtpId))
                    .WhereIf(eventType.HasValue, e => e.EventType == eventType)
                    .WhereIf(!string.IsNullOrWhiteSpace(category), e => e.Category.Contains(category))
                    .WhereIf(!string.IsNullOrWhiteSpace(sendGridEventId), e => e.SendGridEventId.Contains(sendGridEventId))
                    .WhereIf(!string.IsNullOrWhiteSpace(sendGridMessageId), e => e.SendGridMessageId.Contains(sendGridMessageId))
                    .WhereIf(!string.IsNullOrWhiteSpace(tLS), e => e.TLS.Contains(tLS))
                    .WhereIf(!string.IsNullOrWhiteSpace(marketingCampainId), e => e.MarketingCampainId.Contains(marketingCampainId))
                    .WhereIf(!string.IsNullOrWhiteSpace(marketingCampainName), e => e.MarketingCampainName.Contains(marketingCampainName))
                    .WhereIf(isLogSynced.HasValue, e => e.IsLogSynced == isLogSynced);
        }
    }
}