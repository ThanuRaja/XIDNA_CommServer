using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using XICommServer.Shared;

namespace XICommServer.MailEvents
{
    public interface IMailEventsAppService : IApplicationService
    {
        Task<PagedResultDto<MailEventDto>> GetListAsync(GetMailEventsInput input);

        Task<MailEventDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<MailEventDto> CreateAsync(MailEventCreateDto input);

        Task<MailEventDto> UpdateAsync(Guid id, MailEventUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(MailEventExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}