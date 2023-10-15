using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using XICommServer.Shared;

namespace XICommServer.MailEventLogs
{
    public interface IMailEventLogsAppService : IApplicationService
    {
        Task<PagedResultDto<MailEventLogDto>> GetListAsync(GetMailEventLogsInput input);

        Task<MailEventLogDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<MailEventLogDto> CreateAsync(MailEventLogCreateDto input);

        Task<MailEventLogDto> UpdateAsync(Guid id, MailEventLogUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MailEventLogExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}