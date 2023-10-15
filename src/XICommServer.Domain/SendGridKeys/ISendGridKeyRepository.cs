using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace XICommServer.SendGridKeys
{
    public interface ISendGridKeyRepository : IRepository<SendGridKey, Guid>
    {
        Task<List<SendGridKey>> GetListAsync(
            string filterText = null,
            string name = null,
            string aPIKey = null,
            string displayName = null,
            string emailAddress = null,
            bool? isDefault = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string name = null,
            string aPIKey = null,
            string displayName = null,
            string emailAddress = null,
            bool? isDefault = null,
            CancellationToken cancellationToken = default);
    }
}