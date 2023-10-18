using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cotur.Abp.ApiKeyAuthorization.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using Volo.Abp.Application.Dtos;

namespace Cotur.Abp.ApiKeyAuthorization.ApiKeys;

[Authorize(ApiKeyAuthorizationPermissions.ApiKeys.Default)]
[Area(ApiKeyAuthorizationRemoteServiceConsts.ModuleName)]
[ControllerName("ApiKeys")]
[Route("api/api-keys")]
public class ApiKeysController : ApiKeyAuthorizationController, IApiKeysAppService
{
    private readonly IApiKeysAppService _apiKeysAppService;
    private IHostingEnvironment Environment;

    public ApiKeysController(IApiKeysAppService apiKeysAppService, IHostingEnvironment _environment)
    {
        _apiKeysAppService = apiKeysAppService;
        Environment = _environment;
    }

    [HttpGet]
    [Route("{id}")]
    public Task<ApiKeyDto> GetAsync(Guid id)
    {
        return _apiKeysAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<ApiKeyDto>> GetListAsync(PagedAndSortedResultRequestDto input)
    {
        return _apiKeysAppService.GetListAsync(input);
    }

    [HttpPost]
    [Authorize(ApiKeyAuthorizationPermissions.ApiKeys.Create)]
    public Task<ApiKeyDto> CreateAsync(ApiKeyCreateDto input)
    {
        return _apiKeysAppService.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(ApiKeyAuthorizationPermissions.ApiKeys.Update)]
    public Task<ApiKeyDto> UpdateAsync(Guid id, ApiKeyUpdateDto input)
    {
        return _apiKeysAppService.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(ApiKeyAuthorizationPermissions.ApiKeys.Delete)]
    public Task DeleteAsync(Guid id)
    {
        return _apiKeysAppService.DeleteAsync(id);
    }
    [HttpGet("get-api-key-data/{id}")]
    public Task<ApiKeyDto> ApikeysData(Guid Id)
    {
        throw new NotImplementedException();
    }
}