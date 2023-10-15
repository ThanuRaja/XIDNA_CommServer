using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace XICommServer.SendGridKeys
{
    public class SendGridKeyManager : DomainService
    {
        private readonly ISendGridKeyRepository _sendGridKeyRepository;

        public SendGridKeyManager(ISendGridKeyRepository sendGridKeyRepository)
        {
            _sendGridKeyRepository = sendGridKeyRepository;
        }

        public async Task<SendGridKey> CreateAsync(
        string name, string aPIKey, string displayName, string emailAddress, bool isDefault)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(aPIKey, nameof(aPIKey));
            Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress));

            var sendGridKey = new SendGridKey(
             GuidGenerator.Create(),
             name, aPIKey, displayName, emailAddress, isDefault
             );

            return await _sendGridKeyRepository.InsertAsync(sendGridKey);
        }

        public async Task<SendGridKey> UpdateAsync(
            Guid id,
            string name, string aPIKey, string displayName, string emailAddress, bool isDefault, [CanBeNull] string concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(aPIKey, nameof(aPIKey));
            Check.NotNullOrWhiteSpace(emailAddress, nameof(emailAddress));

            var sendGridKey = await _sendGridKeyRepository.GetAsync(id);

            sendGridKey.Name = name;
            sendGridKey.APIKey = aPIKey;
            sendGridKey.DisplayName = displayName;
            sendGridKey.EmailAddress = emailAddress;
            sendGridKey.IsDefault = isDefault;

            sendGridKey.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _sendGridKeyRepository.UpdateAsync(sendGridKey);
        }

    }
}