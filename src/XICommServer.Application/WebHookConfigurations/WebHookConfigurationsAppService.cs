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
using XICommServer.WebHookConfigurations;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using XICommServer.Shared;

namespace XICommServer.WebHookConfigurations
{

    [Authorize(XICommServerPermissions.WebHookConfigurations.Default)]
    public class WebHookConfigurationsAppService : ApplicationService, IWebHookConfigurationsAppService
    {
        private readonly IDistributedCache<WebHookConfigurationExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IWebHookConfigurationRepository _webHookConfigurationRepository;
        private readonly WebHookConfigurationManager _webHookConfigurationManager;

        public WebHookConfigurationsAppService(IWebHookConfigurationRepository webHookConfigurationRepository, WebHookConfigurationManager webHookConfigurationManager, IDistributedCache<WebHookConfigurationExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _webHookConfigurationRepository = webHookConfigurationRepository;
            _webHookConfigurationManager = webHookConfigurationManager;
        }

        public virtual async Task<PagedResultDto<WebHookConfigurationDto>> GetListAsync(GetWebHookConfigurationsInput input)
        {
            var totalCount = await _webHookConfigurationRepository.GetCountAsync(input.FilterText, input.ApiSignatureVerificationKey, input.ClientWebhookUrl, input.ListenProcessed, input.ListenDeferred, input.ListenDelivered, input.ListenOpen, input.ListenClick, input.ListenBounce, input.ListenDropped, input.ListenSpamReport, input.ListenUnsubscribe, input.ListenGroupUnsubscribe, input.ListenGroupResubscribe, input.IsDefault);
            var items = await _webHookConfigurationRepository.GetListAsync(input.FilterText, input.ApiSignatureVerificationKey, input.ClientWebhookUrl, input.ListenProcessed, input.ListenDeferred, input.ListenDelivered, input.ListenOpen, input.ListenClick, input.ListenBounce, input.ListenDropped, input.ListenSpamReport, input.ListenUnsubscribe, input.ListenGroupUnsubscribe, input.ListenGroupResubscribe, input.IsDefault, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<WebHookConfigurationDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WebHookConfiguration>, List<WebHookConfigurationDto>>(items)
            };
        }

        public virtual async Task<WebHookConfigurationDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<WebHookConfiguration, WebHookConfigurationDto>(await _webHookConfigurationRepository.GetAsync(id));
        }

        [Authorize(XICommServerPermissions.WebHookConfigurations.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _webHookConfigurationRepository.DeleteAsync(id);
        }

        [Authorize(XICommServerPermissions.WebHookConfigurations.Create)]
        public virtual async Task<WebHookConfigurationDto> CreateAsync(WebHookConfigurationCreateDto input)
        {

            var webHookConfiguration = await _webHookConfigurationManager.CreateAsync(
            input.ApiSignatureVerificationKey, input.ClientWebhookUrl, input.ListenProcessed, input.ListenDeferred, input.ListenDelivered, input.ListenOpen, input.ListenClick, input.ListenBounce, input.ListenDropped, input.ListenSpamReport, input.ListenUnsubscribe, input.ListenGroupUnsubscribe, input.ListenGroupResubscribe, input.IsDefault
            );

            return ObjectMapper.Map<WebHookConfiguration, WebHookConfigurationDto>(webHookConfiguration);
        }

        [Authorize(XICommServerPermissions.WebHookConfigurations.Edit)]
        public virtual async Task<WebHookConfigurationDto> UpdateAsync(Guid id, WebHookConfigurationUpdateDto input)
        {

            var webHookConfiguration = await _webHookConfigurationManager.UpdateAsync(
            id,
            input.ApiSignatureVerificationKey, input.ClientWebhookUrl, input.ListenProcessed, input.ListenDeferred, input.ListenDelivered, input.ListenOpen, input.ListenClick, input.ListenBounce, input.ListenDropped, input.ListenSpamReport, input.ListenUnsubscribe, input.ListenGroupUnsubscribe, input.ListenGroupResubscribe, input.IsDefault, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<WebHookConfiguration, WebHookConfigurationDto>(webHookConfiguration);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WebHookConfigurationExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _webHookConfigurationRepository.GetListAsync(input.FilterText, input.ApiSignatureVerificationKey, input.ClientWebhookUrl, input.ListenProcessed, input.ListenDeferred, input.ListenDelivered, input.ListenOpen, input.ListenClick, input.ListenBounce, input.ListenDropped, input.ListenSpamReport, input.ListenUnsubscribe, input.ListenGroupUnsubscribe, input.ListenGroupResubscribe, input.IsDefault);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<WebHookConfiguration>, List<WebHookConfigurationExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "WebHookConfigurations.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new WebHookConfigurationExcelDownloadTokenCacheItem { Token = token },
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