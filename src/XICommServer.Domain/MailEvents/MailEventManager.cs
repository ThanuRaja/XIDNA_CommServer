using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace XICommServer.MailEvents
{
    public class MailEventManager : DomainService
    {
        private readonly IMailEventRepository _mailEventRepository;

        public MailEventManager(IMailEventRepository mailEventRepository)
        {
            _mailEventRepository = mailEventRepository;
        }

        public async Task<MailEvent> CreateAsync(
        string sGMessageId, bool isSuccess, DateTime? createdAt = null)
        {

            var mailEvent = new MailEvent(
             GuidGenerator.Create(),
             sGMessageId, isSuccess, createdAt
             );

            return await _mailEventRepository.InsertAsync(mailEvent);
        }

        public async Task<MailEvent> UpdateAsync(
            Guid id,
            string sGMessageId, bool isSuccess, DateTime? createdAt = null, [CanBeNull] string concurrencyStamp = null
        )
        {

            var mailEvent = await _mailEventRepository.GetAsync(id);

            mailEvent.SGMessageId = sGMessageId;
            mailEvent.IsSuccess = isSuccess;
            mailEvent.CreatedAt = createdAt;

            mailEvent.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _mailEventRepository.UpdateAsync(mailEvent);
        }

    }
}