using System.Web;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.WordPress.Utilities
{
    public static class PostExtensions
    {
        public static string GetTitle(this Post post)
        {
            //Post Titles in WordPress have "en dashes" (–) instead of "hephens" (-)
            string title = HttpUtility.HtmlDecode(post.Title.Rendered).Replace('–', '-');

            return title;
        }

        public static string[] GetSchoolUnique(this Post post)
        {
            string title = post.GetTitle();
            
            //TODO: Change specials (__ and _) in post titles
            return title.Split("__")[0]
                        .Split('_');
        }
    }
}
