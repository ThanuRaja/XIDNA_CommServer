using XICommServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace XICommServer.MailEventLogs
{
    public class MailEventLogManager : DomainService
    {
        private readonly IMailEventLogRepository _mailEventLogRepository;

        public MailEventLogManager(IMailEventLogRepository mailEventLogRepository)
        {
            _mailEventLogRepository = mailEventLogRepository;
        }

        public async Task<MailEventLog> CreateAsync(
        DateTime timestamp, string smtpId, EventType eventType, string category, string sendGridEventId, string sendGridMessageId, string tLS, string marketingCampainId, string marketingCampainName, bool isLogSynced)
        {
            Check.NotNull(timestamp, nameof(timestamp));
            Check.NotNull(eventType, nameof(eventType));

            var mailEventLog = new MailEventLog(
             GuidGenerator.Create(),
             timestamp, smtpId, eventType, category, sendGridEventId, sendGridMessageId, tLS, marketingCampainId, marketingCampainName, isLogSynced
             );

            return await _mailEventLogRepository.InsertAsync(mailEventLog);
        }

        public async Task<MailEventLog> UpdateAsync(
            Guid id,
            DateTime timestamp, string smtpId, EventType eventType, string category, string sendGridEventId, string sendGridMessageId, string tLS, string marketingCampainId, string marketingCampainName, bool isLogSynced, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNull(timestamp, nameof(timestamp));
            Check.NotNull(eventType, nameof(eventType));

            var mailEventLog = await _mailEventLogRepository.GetAsync(id);

            mailEventLog.Timestamp = timestamp;
            mailEventLog.SmtpId = smtpId;
            mailEventLog.EventType = eventType;
            mailEventLog.Category = category;
            mailEventLog.SendGridEventId = sendGridEventId;
            mailEventLog.SendGridMessageId = sendGridMessageId;
            mailEventLog.TLS = tLS;
            mailEventLog.MarketingCampainId = marketingCampainId;
            mailEventLog.MarketingCampainName = marketingCampainName;
            mailEventLog.IsLogSynced = isLogSynced;

            mailEventLog.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mailEventLogRepository.UpdateAsync(mailEventLog);
        }

    }
}