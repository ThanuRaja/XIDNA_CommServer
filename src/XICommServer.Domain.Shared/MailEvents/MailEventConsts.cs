namespace XICommServer.MailEvents
{
    public static class MailEventConsts
    {
        private const string DefaultSorting = "{0}SGMessageId asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "MailEvent." : string.Empty);
        }

    }
}