using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using XICommServer.Permissions;
using XICommServer.SendGridKeys;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using XICommServer.Shared;

namespace XICommServer.SendGridKeys
{

    [Authorize(XICommServerPermissions.SendGridKeys.Default)]
    public class SendGridKeysAppService : ApplicationService, ISendGridKeysAppService
    {
        private readonly IDistributedCache<SendGridKeyExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly ISendGridKeyRepository _sendGridKeyRepository;
        private readonly SendGridKeyManager _sendGridKeyManager;

        public SendGridKeysAppService(ISendGridKeyRepository sendGridKeyRepository, SendGridKeyManager sendGridKeyManager, IDistributedCache<SendGridKeyExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _sendGridKeyRepository = sendGridKeyRepository;
            _sendGridKeyManager = sendGridKeyManager;
        }

        public virtual async Task<PagedResultDto<SendGridKeyDto>> GetListAsync(GetSendGridKeysInput input)
        {
            var totalCount = await _sendGridKeyRepository.GetCountAsync(input.FilterText, input.Name, input.APIKey, input.DisplayName, input.EmailAddress, input.IsDefault);
            var items = await _sendGridKeyRepository.GetListAsync(input.FilterText, input.Name, input.APIKey, input.DisplayName, input.EmailAddress, input.IsDefault, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<SendGridKeyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SendGridKey>, List<SendGridKeyDto>>(items)
            };
        }

        public virtual async Task<SendGridKeyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<SendGridKey, SendGridKeyDto>(await _sendGridKeyRepository.GetAsync(id));
        }

        [Authorize(XICommServerPermissions.SendGridKeys.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _sendGridKeyRepository.DeleteAsync(id);
        }

        [Authorize(XICommServerPermissions.SendGridKeys.Create)]
        public virtual async Task<SendGridKeyDto> CreateAsync(SendGridKeyCreateDto input)
        {

            var sendGridKey = await _sendGridKeyManager.CreateAsync(
            input.Name, input.APIKey, input.DisplayName, input.EmailAddress, input.IsDefault
            );

            return ObjectMapper.Map<SendGridKey, SendGridKeyDto>(sendGridKey);
        }

        [Authorize(XICommServerPermissions.SendGridKeys.Edit)]
        public virtual async Task<SendGridKeyDto> UpdateAsync(Guid id, SendGridKeyUpdateDto input)
        {

            var sendGridKey = await _sendGridKeyManager.UpdateAsync(
            id,
            input.Name, input.APIKey, input.DisplayName, input.EmailAddress, input.IsDefault, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<SendGridKey, SendGridKeyDto>(sendGridKey);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(SendGridKeyExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _sendGridKeyRepository.GetListAsync(input.FilterText, input.Name, input.APIKey, input.DisplayName, input.EmailAddress, input.IsDefault);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<SendGridKey>, List<SendGridKeyExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "SendGridKeys.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new SendGridKeyExcelDownloadTokenCacheItem { Token = token },
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
                });

            return new DownloadTokenResultDto
            {
                Token = token
            };
        }
    }
}