namespace XICommServer.Permissions;

public static class XICommServerPermissions
{
    public const string GroupName = "XICommServer";

    public static class Dashboard
    {
        public const string DashboardGroup = GroupName + ".Dashboard";
        public const string Host = DashboardGroup + ".Host";
        public const string Tenant = DashboardGroup + ".Tenant";
    }

    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";

    public static class SendGridKeys
    {
        public const string Default = GroupName + ".SendGridKeys";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class MailEvents
    {
        public const string Default = GroupName + ".MailEvents";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class WebHookConfigurations
    {
        public const string Default = GroupName + ".WebHookConfigurations";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class MailEventLogs
    {
        public const string Default = GroupName + ".MailEventLogs";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}