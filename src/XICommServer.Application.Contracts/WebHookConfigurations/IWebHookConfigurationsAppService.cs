using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using XICommServer.Shared;

namespace XICommServer.WebHookConfigurations
{
    public interface IWebHookConfigurationsAppService : IApplicationService
    {
        Task<PagedResultDto<WebHookConfigurationDto>> GetListAsync(GetWebHookConfigurationsInput input);

        Task<WebHookConfigurationDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<WebHookConfigurationDto> CreateAsync(WebHookConfigurationCreateDto input);

        Task<WebHookConfigurationDto> UpdateAsync(Guid id, WebHookConfigurationUpdateDto input);

        Task<IRemoteStreamContent> GetListAsExcelFileAsync(WebHookConfigurationExcelDownloadDto input);

        Task<DownloadTokenResultDto> GetDownloadTokenAsync();
    }
}