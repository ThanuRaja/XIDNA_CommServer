using XICommServer.Web.Pages.MailEventLogs;
using XICommServer.MailEventLogs;
using XICommServer.Web.Pages.WebHookConfigurations;
using XICommServer.WebHookConfigurations;
using XICommServer.Web.Pages.MailEvents;
using XICommServer.MailEvents;
using XICommServer.Web.Pages.SendGridKeys;
using Volo.Abp.AutoMapper;
using XICommServer.SendGridKeys;
using AutoMapper;

namespace XICommServer.Web;

public class XICommServerWebAutoMapperProfile : Profile
{
    public XICommServerWebAutoMapperProfile()
    {
        //Define your object mappings here, for the Web project

        CreateMap<SendGridKeyDto, SendGridKeyUpdateViewModel>();
        CreateMap<SendGridKeyUpdateViewModel, SendGridKeyUpdateDto>();
        CreateMap<SendGridKeyCreateViewModel, SendGridKeyCreateDto>();

        CreateMap<MailEventDto, MailEventUpdateViewModel>();
        CreateMap<MailEventUpdateViewModel, MailEventUpdateDto>();
        CreateMap<MailEventCreateViewModel, MailEventCreateDto>();

        CreateMap<WebHookConfigurationDto, WebHookConfigurationUpdateViewModel>();
        CreateMap<WebHookConfigurationUpdateViewModel, WebHookConfigurationUpdateDto>();
        CreateMap<WebHookConfigurationCreateViewModel, WebHookConfigurationCreateDto>();

        CreateMap<MailEventLogDto, MailEventLogUpdateViewModel>();
        CreateMap<MailEventLogUpdateViewModel, MailEventLogUpdateDto>();
        CreateMap<MailEventLogCreateViewModel, MailEventLogCreateDto>();
    }
}