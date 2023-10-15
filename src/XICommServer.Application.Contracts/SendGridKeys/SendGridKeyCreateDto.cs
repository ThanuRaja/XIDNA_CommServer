using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace XICommServer.SendGridKeys
{
    public class SendGridKeyCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string APIKey { get; set; }
        public string? DisplayName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public bool IsDefault { get; set; }
    }
}