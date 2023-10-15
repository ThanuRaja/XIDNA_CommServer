using System;

namespace XICommServer.SendGridKeys
{
    public class SendGridKeyExcelDto
    {
        public string Name { get; set; }
        public string APIKey { get; set; }
        public string? DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsDefault { get; set; }
    }
}