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

namespace XICommServer.SendGridKeys
{
    public class EfCoreSendGridKeyRepository : EfCoreRepository<XICommServerDbContext, SendGridKey, Guid>, ISendGridKeyRepository
    {
        public EfCoreSendGridKeyRepository(IDbContextProvider<XICommServerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<SendGridKey>> GetListAsync(
            string filterText = null,
            string name = null,
            string aPIKey = null,
            string displayName = null,
            string emailAddress = null,
            bool? isDefault = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, name, aPIKey, displayName, emailAddress, isDefault);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? SendGridKeyConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public async Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string aPIKey = null,
            string displayName = null,
            string emailAddress = null,
            bool? isDefault = null,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetDbSetAsync()), filterText, name, aPIKey, displayName, emailAddress, isDefault);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<SendGridKey> ApplyFilter(
            IQueryable<SendGridKey> query,
            string filterText,
            string name = null,
            string aPIKey = null,
            string displayName = null,
            string emailAddress = null,
            bool? isDefault = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name.Contains(filterText) || e.APIKey.Contains(filterText) || e.DisplayName.Contains(filterText) || e.EmailAddress.Contains(filterText))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(aPIKey), e => e.APIKey.Contains(aPIKey))
                    .WhereIf(!string.IsNullOrWhiteSpace(displayName), e => e.DisplayName.Contains(displayName))
                    .WhereIf(!string.IsNullOrWhiteSpace(emailAddress), e => e.EmailAddress.Contains(emailAddress))
                    .WhereIf(isDefault.HasValue, e => e.IsDefault == isDefault);
        }
    }
}