using Volo.Abp.Settings;

namespace XICommServer.Settings;

public class XICommServerSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(XICommServerSettings.MySetting1));
    }
}
