using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace XICommServer.SendGridKeys
{
    public class SendGridKeyUpdateDto : IHasConcurrencyStamp
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string APIKey { get; set; }
        public string? DisplayName { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public bool IsDefault { get; set; }

        public string ConcurrencyStamp { get; set; }
    }
}