using Volo.Abp.Application.Dtos;
using System;

namespace XICommServer.SendGridKeys
{
    public class GetSendGridKeysInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? Name { get; set; }
        public string? APIKey { get; set; }
        public string? DisplayName { get; set; }
        public string? EmailAddress { get; set; }
        public bool? IsDefault { get; set; }

        public GetSendGridKeysInput()
        {

        }
    }
}