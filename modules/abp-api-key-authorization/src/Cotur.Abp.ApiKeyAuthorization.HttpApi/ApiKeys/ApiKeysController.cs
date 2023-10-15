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

    [HttpGet]
    [Route("generate-apiconfig-file")]
    public async Task<JsonResult> GenerateApiConfigFile(Guid id)
    {
        var data = await _apiKeysAppService.ApikeysData(id);
        
        //// Show the IP 
        var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
        //var localIPAddr = feature?.LocalIpAddress?.ToString();
        //var localPort = feature?.LocalPort.ToString();

        //var clientIpAddr = feature?.RemoteIpAddress?.ToString();
        //var clientPort = feature?.RemotePort.ToString();

        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipv4Address = ipHostInfo.AddressList[0];
        var localPort = HttpContext.Request.Host.Port.ToString();
        var localhostAddressName = HttpContext.Request.Host.Host.ToString();

        var apiconfig = new ApiConfig();
        var path = this.Environment.ContentRootPath;//AppContext.BaseDirectory;
        var apikey = data.Key;
        var resp = apiconfig.CreateAPIConfigFile(path, localhostAddressName, localPort=="" ? "443" : localPort , apikey);
        
        if(resp ==0)
        {
            var filePath = Path.Combine(path, "api_config.json");
            //string contents = File.ReadAllText(@"C:\temp\test.txt");
            using (StreamReader file = new StreamReader(filePath))
            {
                var fileData = file.ReadToEnd();
                var dataInBytes = Encoding.UTF8.GetBytes(fileData);
                var str = Encoding.Default.GetString(dataInBytes);

                // Converting into file format...
                string contentType = "application/json";
                string fileName = "api_config.json";
                var fileDataResp = new FileDataDto()
                {
                    FileContent = dataInBytes,
                    FileContentType = contentType,
                    FileName = fileName,
                };
                return new JsonResult(fileDataResp);
            }

        }
        else
        {
            return new JsonResult("false");
        }
        
    }

    [HttpGet]
    [Route("removing-apiconfig-file")]
    public async Task<bool> RemoveApiConfigFile()
    {

        var path = AppContext.BaseDirectory;
        var filePath = Path.Combine(path, "api_config.json");
        System.IO.File.Delete(filePath);
        return true;
       

    }

    public Task<ApiKeyDto> ApikeysData(Guid Id)
    {
        throw new NotImplementedException();
    }
}