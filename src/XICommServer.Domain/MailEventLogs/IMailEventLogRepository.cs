using XICommServer;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace XICommServer.MailEventLogs
{
    public interface IMailEventLogRepository : IRepository<MailEventLog, Guid>
    {
        Task<List<MailEventLog>> GetListAsync(
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
            CancellationToken cancellationToken = default
        );

        Task<long> GetCountAsync(
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
            CancellationToken cancellationToken = default);
    }
}