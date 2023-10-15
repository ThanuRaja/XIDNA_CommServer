namespace XICommServer.WebHookConfigurations
{
    public static class WebHookConfigurationConsts
    {
        private const string DefaultSorting = "{0}ApiSignatureVerificationKey asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "WebHookConfiguration." : string.Empty);
        }

    }
}