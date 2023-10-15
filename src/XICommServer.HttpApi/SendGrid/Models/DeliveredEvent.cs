using Microsoft.Extensions.Logging;

namespace XICommServer
{
    public class DeliveredEvent : Event
    {
        public string Response { get; set; }
    }
}
