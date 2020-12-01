using System.Web;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.WordPress.Utilities
{
    public static class PostExtensions
    {
        private const string primaryDelimiter = "__";
        private const string secondaryDelimiter = "_";

        public static string GetTitle(this Post post)
        {
            //Post Titles in WordPress have "en dashes" (–) instead of "hephens" (-)
            string title = HttpUtility.HtmlDecode(post.Title.Rendered).Replace('–', '-');

            return title;
        }

        public static Post CreatePostForTestUser(this Post post)
        {
            return new Post() { Title = new Title(post.GetTitle().Split(primaryDelimiter)[0] + primaryDelimiter + "User_0"), Id = -1 };
        }

        public static string[] GetSchoolUnique(this Post post)
        {
            string title = post.GetTitle();
            
            //TODO: Change specials (__ and _) in post titles
            return title.Split(primaryDelimiter)[0]
                        .Split(secondaryDelimiter);
        }
    }
}
