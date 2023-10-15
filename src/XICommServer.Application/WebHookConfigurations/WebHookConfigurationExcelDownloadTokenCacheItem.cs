using System;

namespace XICommServer.WebHookConfigurations;

[Serializable]
public class WebHookConfigurationExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}