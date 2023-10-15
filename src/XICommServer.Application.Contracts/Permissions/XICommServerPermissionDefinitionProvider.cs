using XICommServer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace XICommServer.Permissions;

public class XICommServerPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(XICommServerPermissions.GroupName);

        myGroup.AddPermission(XICommServerPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
        myGroup.AddPermission(XICommServerPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(XICommServerPermissions.MyPermission1, L("Permission:MyPermission1"));

        var sendGridKeyPermission = myGroup.AddPermission(XICommServerPermissions.SendGridKeys.Default, L("Permission:SendGridKeys"));
        sendGridKeyPermission.AddChild(XICommServerPermissions.SendGridKeys.Create, L("Permission:Create"));
        sendGridKeyPermission.AddChild(XICommServerPermissions.SendGridKeys.Edit, L("Permission:Edit"));
        sendGridKeyPermission.AddChild(XICommServerPermissions.SendGridKeys.Delete, L("Permission:Delete"));

        var mailEventPermission = myGroup.AddPermission(XICommServerPermissions.MailEvents.Default, L("Permission:MailEvents"));
        mailEventPermission.AddChild(XICommServerPermissions.MailEvents.Create, L("Permission:Create"));
        mailEventPermission.AddChild(XICommServerPermissions.MailEvents.Edit, L("Permission:Edit"));
        mailEventPermission.AddChild(XICommServerPermissions.MailEvents.Delete, L("Permission:Delete"));

        var webHookConfigurationPermission = myGroup.AddPermission(XICommServerPermissions.WebHookConfigurations.Default, L("Permission:WebHookConfigurations"));
        webHookConfigurationPermission.AddChild(XICommServerPermissions.WebHookConfigurations.Create, L("Permission:Create"));
        webHookConfigurationPermission.AddChild(XICommServerPermissions.WebHookConfigurations.Edit, L("Permission:Edit"));
        webHookConfigurationPermission.AddChild(XICommServerPermissions.WebHookConfigurations.Delete, L("Permission:Delete"));

        var mailEventLogPermission = myGroup.AddPermission(XICommServerPermissions.MailEventLogs.Default, L("Permission:MailEventLogs"));
        mailEventLogPermission.AddChild(XICommServerPermissions.MailEventLogs.Create, L("Permission:Create"));
        mailEventLogPermission.AddChild(XICommServerPermissions.MailEventLogs.Edit, L("Permission:Edit"));
        mailEventLogPermission.AddChild(XICommServerPermissions.MailEventLogs.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<XICommServerResource>(name);
    }
}