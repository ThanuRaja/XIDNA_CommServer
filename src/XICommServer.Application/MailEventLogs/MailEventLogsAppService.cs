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
using XICommServer.MailEventLogs;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using XICommServer.Shared;

namespace XICommServer.MailEventLogs
{

    [Authorize(XICommServerPermissions.MailEventLogs.Default)]
    public class MailEventLogsAppService : ApplicationService, IMailEventLogsAppService
    {
        private readonly IDistributedCache<MailEventLogExcelDownloadTokenCacheItem, string> _excelDownloadTokenCache;
        private readonly IMailEventLogRepository _mailEventLogRepository;
        private readonly MailEventLogManager _mailEventLogManager;

        public MailEventLogsAppService(IMailEventLogRepository mailEventLogRepository, MailEventLogManager mailEventLogManager, IDistributedCache<MailEventLogExcelDownloadTokenCacheItem, string> excelDownloadTokenCache)
        {
            _excelDownloadTokenCache = excelDownloadTokenCache;
            _mailEventLogRepository = mailEventLogRepository;
            _mailEventLogManager = mailEventLogManager;
        }

        public virtual async Task<PagedResultDto<MailEventLogDto>> GetListAsync(GetMailEventLogsInput input)
        {
            var totalCount = await _mailEventLogRepository.GetCountAsync(input.FilterText, input.TimestampMin, input.TimestampMax, input.SmtpId, input.EventType, input.Category, input.SendGridEventId, input.SendGridMessageId, input.TLS, input.MarketingCampainId, input.MarketingCampainName, input.IsLogSynced);
            var items = await _mailEventLogRepository.GetListAsync(input.FilterText, input.TimestampMin, input.TimestampMax, input.SmtpId, input.EventType, input.Category, input.SendGridEventId, input.SendGridMessageId, input.TLS, input.MarketingCampainId, input.MarketingCampainName, input.IsLogSynced, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<MailEventLogDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<MailEventLog>, List<MailEventLogDto>>(items)
            };
        }

        public virtual async Task<MailEventLogDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<MailEventLog, MailEventLogDto>(await _mailEventLogRepository.GetAsync(id));
        }

        [Authorize(XICommServerPermissions.MailEventLogs.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _mailEventLogRepository.DeleteAsync(id);
        }

        [Authorize(XICommServerPermissions.MailEventLogs.Create)]
        public virtual async Task<MailEventLogDto> CreateAsync(MailEventLogCreateDto input)
        {

            var mailEventLog = await _mailEventLogManager.CreateAsync(
            input.Timestamp, input.SmtpId, input.EventType, input.Category, input.SendGridEventId, input.SendGridMessageId, input.TLS, input.MarketingCampainId, input.MarketingCampainName, input.IsLogSynced
            );

            return ObjectMapper.Map<MailEventLog, MailEventLogDto>(mailEventLog);
        }

        [Authorize(XICommServerPermissions.MailEventLogs.Edit)]
        public virtual async Task<MailEventLogDto> UpdateAsync(Guid id, MailEventLogUpdateDto input)
        {

            var mailEventLog = await _mailEventLogManager.UpdateAsync(
            id,
            input.Timestamp, input.SmtpId, input.EventType, input.Category, input.SendGridEventId, input.SendGridMessageId, input.TLS, input.MarketingCampainId, input.MarketingCampainName, input.IsLogSynced, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<MailEventLog, MailEventLogDto>(mailEventLog);
        }

        [AllowAnonymous]
        public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MailEventLogExcelDownloadDto input)
        {
            var downloadToken = await _excelDownloadTokenCache.GetAsync(input.DownloadToken);
            if (downloadToken == null || input.DownloadToken != downloadToken.Token)
            {
                throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
            }

            var items = await _mailEventLogRepository.GetListAsync(input.FilterText, input.TimestampMin, input.TimestampMax, input.SmtpId, input.EventType, input.Category, input.SendGridEventId, input.SendGridMessageId, input.TLS, input.MarketingCampainId, input.MarketingCampainName, input.IsLogSynced);

            var memoryStream = new MemoryStream();
            await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MailEventLog>, List<MailEventLogExcelDto>>(items));
            memoryStream.Seek(0, SeekOrigin.Begin);

            return new RemoteStreamContent(memoryStream, "MailEventLogs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public async Task<DownloadTokenResultDto> GetDownloadTokenAsync()
        {
            var token = Guid.NewGuid().ToString("N");

            await _excelDownloadTokenCache.SetAsync(
                token,
                new MailEventLogExcelDownloadTokenCacheItem { Token = token },
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