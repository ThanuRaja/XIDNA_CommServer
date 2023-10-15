using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using XICommServer.Localization;
using XICommServer.Permissions;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Navigation;
using Cotur.Abp.ApiKeyAuthorization.Web.Menus;

namespace XICommServer.Web.Menus;

public class XICommServerMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<XICommServerResource>();

        //Home
        context.Menu.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.Home,
                l["Menu:Home"],
                "~/",
                icon: "fa fa-home",
                order: 1
            )
        );

        //HostDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.HostDashboard,
                l["Menu:Dashboard"],
                "~/HostDashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(XICommServerPermissions.Dashboard.Host)
        );

        //TenantDashboard
        context.Menu.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.TenantDashboard,
                l["Menu:Dashboard"],
                "~/Dashboard",
                icon: "fa fa-line-chart",
                order: 2
            ).RequirePermissions(XICommServerPermissions.Dashboard.Tenant)
        );

        context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;

        //Administration->Identity

        var sas = context.Menu.GetMenuItem(SaasHostMenuNames.GroupName);
        sas.Items.Clear();

        var openid = administration.GetMenuItem(OpenIddictProMenus.GroupName);
        openid.Items.Clear();

        administration.TryRemoveMenuItem(LanguageManagementMenuNames.GroupName);
        administration.TryRemoveMenuItem(TextTemplateManagementMainMenuNames.GroupName);
        administration.TryRemoveMenuItem(AbpAuditLoggingMainMenuNames.GroupName);

        var identitySection = administration.GetMenuItem(IdentityMenuNames.GroupName);

        identitySection.TryRemoveMenuItem(IdentityMenuNames.SecurityLogs);
        identitySection.TryRemoveMenuItem(IdentityMenuNames.ClaimTypes);
        identitySection.TryRemoveMenuItem(IdentityMenuNames.OrganizationUnits);

        administration.SetSubItemOrder(ApiKeyAuthorizationMenuNames.GroupName, 6);

        administration.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.SendGridKeys,
                l["Menu:SendGridKeys"],
                url: "/SendGridKeys",
                icon: "fa fa-file-alt",
                requiredPermissionName: XICommServerPermissions.SendGridKeys.Default)
        );

        administration.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.MailEvents,
                l["Menu:MailEvents"],
                url: "/MailEvents",
                icon: "fa fa-file-alt",
                requiredPermissionName: XICommServerPermissions.MailEvents.Default)
        );

        administration.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.WebHookConfigurations,
                l["Menu:WebHookConfigurations"],
                url: "/WebHookConfigurations",
                icon: "fa fa-file-alt",
                requiredPermissionName: XICommServerPermissions.WebHookConfigurations.Default)
        );

        administration.AddItem(
            new ApplicationMenuItem(
                XICommServerMenus.MailEventLogs,
                l["Menu:MailEventLogs"],
                url: "/MailEventLogs",
                icon: "fa fa-file-alt",
                requiredPermissionName: XICommServerPermissions.MailEventLogs.Default)
        );
        return Task.CompletedTask;
    }
}