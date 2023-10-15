using Newtonsoft.Json;
using System;

namespace XICommServer
{
    public class ClickEvent : OpenEvent
    {
        [JsonConverter(typeof(UriConverter))]
        public Uri Url { get; set; }
    }
}
