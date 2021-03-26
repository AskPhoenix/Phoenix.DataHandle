using System.Web;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.WordPress.Utilities
{
    public static class PostExtensions
    {
        public const string PrimaryDelimiter = "-_-";
        public const string SecondaryDelimiter = "---";

        public static string GetTitle(this Post post)
        {
            //Post Titles in WordPress have "en dashes" (–) instead of "hephens" (-)
            return HttpUtility.HtmlDecode(post.Title.Rendered).Replace('–', '-');
        }
    }
}
