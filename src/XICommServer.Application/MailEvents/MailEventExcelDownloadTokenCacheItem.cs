using System;

namespace XICommServer.MailEvents;

[Serializable]
public class MailEventExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}