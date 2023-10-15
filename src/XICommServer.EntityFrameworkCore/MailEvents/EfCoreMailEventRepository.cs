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

namespace XICommServer.MailEvents
{
    public class EfCoreMailEventRepository : EfCoreRepository<XICommServerDbContext, MailEvent, Guid>, IMailEventRepository
    {
        public EfCoreMailEventRepository(IDbContextProvider<XICommServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<MailEvent>> GetListAsync(
            string filterText = null,
            string sGMessageId = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            bool? isSuccess = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, sGMessageId, createdAtMin, createdAtMax, isSuccess);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MailEventConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string sGMessageId = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            bool? isSuccess = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, sGMessageId, createdAtMin, createdAtMax, isSuccess);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<MailEvent> ApplyFilter(
            IQueryable<MailEvent> query,
            string filterText,
            string sGMessageId = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            bool? isSuccess = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SGMessageId.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(sGMessageId), e => e.SGMessageId.Contains(sGMessageId))
                    .WhereIf(createdAtMin.HasValue, e => e.CreatedAt >= createdAtMin.Value)
                    .WhereIf(createdAtMax.HasValue, e => e.CreatedAt <= createdAtMax.Value)
                    .WhereIf(isSuccess.HasValue, e => e.IsSuccess == isSuccess);
        }
    }
}