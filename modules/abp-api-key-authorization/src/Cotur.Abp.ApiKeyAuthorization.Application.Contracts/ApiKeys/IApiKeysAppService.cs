using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using static Cotur.Abp.ApiKeyAuthorization.Permissions.ApiKeyAuthorizationPermissions;

namespace Cotur.Abp.ApiKeyAuthorization.ApiKeys;

public interface IApiKeysAppService : ICrudAppService<ApiKeyDto, Guid, PagedAndSortedResultRequestDto, ApiKeyCreateDto, ApiKeyUpdateDto>
{
    Task<ApiKeyDto> ApikeysData(Guid Id);
}