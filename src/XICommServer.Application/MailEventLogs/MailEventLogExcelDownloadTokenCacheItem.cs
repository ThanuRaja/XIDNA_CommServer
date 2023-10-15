using System;

namespace XICommServer.MailEventLogs;

[Serializable]
public class MailEventLogExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}