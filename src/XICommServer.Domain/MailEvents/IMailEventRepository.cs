using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace XICommServer.MailEvents
{
    public interface IMailEventRepository : IRepository<MailEvent, Guid>
    {
        Task<List<MailEvent>> GetListAsync(
            string filterText = null,
            string sGMessageId = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            bool? isSuccess = null,
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
            string filterText = null,
            string sGMessageId = null,
            DateTime? createdAtMin = null,
            DateTime? createdAtMax = null,
            bool? isSuccess = null,
            CancellationToken cancellationToken = default);
    }
}