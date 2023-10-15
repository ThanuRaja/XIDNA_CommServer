using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using XICommServer.Shared;

namespace XICommServer.SendGridKeys
{
    public interface ISendGridKeysAppService : IApplicationService
    {
        Task<PagedResultDto<SendGridKeyDto>> GetListAsync(GetSendGridKeysInput input);

        Task<SendGridKeyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SendGridKeyDto> CreateAsync(SendGridKeyCreateDto input);

        Task<SendGridKeyDto> UpdateAsync(Guid id, SendGridKeyUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(SendGridKeyExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}