using System;

namespace XICommServer.SendGridKeys;

[Serializable]
public class SendGridKeyExcelDownloadTokenCacheItem
{
    public string Token { get; set; }
}