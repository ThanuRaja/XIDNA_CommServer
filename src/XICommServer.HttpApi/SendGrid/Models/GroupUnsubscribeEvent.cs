using Newtonsoft.Json;

namespace XICommServer
{
    public class GroupUnsubscribeEvent : ClickEvent
    {
        [JsonProperty("asm_group_id")]
        public int AsmGroupId { get; set; }
    }
}
