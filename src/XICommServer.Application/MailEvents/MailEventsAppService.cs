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
using XICommServer.MailEvents;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using XICommServer.Shared;

namespace XICommServer.MailEvents
{

    [Authorize(XICommServerPermissions.MailEvents.Default)]
    public class MailEventsAppService : ApplicationService, IMailEventsAppService
    {
        private readonly IDistributedCache<MailEventExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMailEventRepository _mailEventRepository;
        private readonly MailEventManager _mailEventManager;

        public MailEventsAppService(IMailEventRepository mailEventRepository, MailEventManager mailEventManager, IDistributedCache<MailEventExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mailEventRepository = mailEventRepository;
            _mailEventManager = mailEventManager;
        }

        public virtual async Task<PagedResultDto<MailEventDto>> GetListAsync(GetMailEventsInput input)
        {
            var totalCount = await _mailEventRepository.GetCountAsync(input.FilterText, input.SGMessageId, input.CreatedAtMin, input.CreatedAtMax, input.IsSuccess);
            var items = await _mailEventRepository.GetListAsync(input.FilterText, input.SGMessageId, input.CreatedAtMin, input.CreatedAtMax, input.IsSuccess, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MailEventDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MailEvent>, List<MailEventDto>>(items)
            };
        }

        public virtual async Task<MailEventDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MailEvent, MailEventDto>(await _mailEventRepository.GetAsync(id));
        }

        [Authorize(XICommServerPermissions.MailEvents.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mailEventRepository.DeleteAsync(id);
        }

        [Authorize(XICommServerPermissions.MailEvents.Create)]
        public virtual async Task<MailEventDto> CreateAsync(MailEventCreateDto input)
        {

            var mailEvent = await _mailEventManager.CreateAsync(
            input.SGMessageId, input.IsSuccess, input.CreatedAt
            );

            return ObjectMapper.Map<MailEvent, MailEventDto>(mailEvent);
        }

        [Authorize(XICommServerPermissions.MailEvents.Edit)]
        public virtual async Task<MailEventDto> UpdateAsync(Guid id, MailEventUpdateDto input)
        {

            var mailEvent = await _mailEventManager.UpdateAsync(
            id,
            input.SGMessageId, input.IsSuccess, input.CreatedAt, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MailEvent, MailEventDto>(mailEvent);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MailEventExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _mailEventRepository.GetListAsync(input.FilterText, input.SGMessageId, input.CreatedAtMin, input.CreatedAtMax, input.IsSuccess);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MailEvent>, List<MailEventExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MailEvents.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MailEventExcelDownloadTokenCacheItem { Token = token },
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