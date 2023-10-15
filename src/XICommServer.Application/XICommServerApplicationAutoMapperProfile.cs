using XICommServer.MailEventLogs;
using XICommServer.WebHookConfigurations;
using XICommServer.MailEvents;
using System;
using XICommServer.Shared;
using Volo.Abp.AutoMapper;
using XICommServer.SendGridKeys;
using AutoMapper;

namespace XICommServer;

public class XICommServerApplicationAutoMapperProfile : Profile
{
    public XICommServerApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<SendGridKey, SendGridKeyDto>();
        CreateMap<SendGridKey, SendGridKeyExcelDto>();

        CreateMap<MailEvent, MailEventDto>();
        CreateMap<MailEvent, MailEventExcelDto>();

        CreateMap<WebHookConfiguration, WebHookConfigurationDto>();
        CreateMap<WebHookConfiguration, WebHookConfigurationExcelDto>();

        CreateMap<MailEventLog, MailEventLogDto>();
        CreateMap<MailEventLog, MailEventLogExcelDto>();
    }
}