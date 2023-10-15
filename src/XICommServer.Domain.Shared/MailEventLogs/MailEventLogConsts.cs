namespace XICommServer.MailEventLogs
{
    public static class MailEventLogConsts
    {
        private const string DefaultSorting = "{0}Timestamp asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MailEventLog." : string.Empty);
        }

    }
}