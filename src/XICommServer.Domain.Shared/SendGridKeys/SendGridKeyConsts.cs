namespace XICommServer.SendGridKeys
{
    public static class SendGridKeyConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "SendGridKey." : string.Empty);
        }

    }
}