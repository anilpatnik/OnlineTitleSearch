namespace OTS.Services.Common
{
    public class AppConstants
    {
        public const string CITE_PATTERN = @"<cite\s*(.+?)\s*>*</cite>";
        public const string HREF_PATTERN = @"<a[^>]+href\s*=\s*([^>])(https?:\/\/.+?)\1>(.*?)</a>";

        public const string SITE_NAME = "infotrack";
    }
}
