using Volo.Abp.Application.Dtos;
using System;

namespace XICommServer.MailEvents
{
    public class GetMailEventsInput : PagedAndSortedResultRequestDto
    {
        public string? FilterText { get; set; }

        public string? SGMessageId { get; set; }
        public DateTime? CreatedAtMin { get; set; }
        public DateTime? CreatedAtMax { get; set; }
        public bool? IsSuccess { get; set; }

        public GetMailEventsInput()
        {

        }
    }
}