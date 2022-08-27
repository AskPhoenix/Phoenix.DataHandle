using System.Text.RegularExpressions;
using System.Web;
using WordPressPCL.Models;

namespace Phoenix.DataHandle.DataEntry
{
    public static class PostExtensions
    {
        private const string businessUQRgxStr = @"^[SP][0-9]+";

        public static readonly Regex BusinessUQRgx = new(businessUQRgxStr);
        public static readonly Regex CourseUQRgx = new(businessUQRgxStr + @"_Course-[0-9]+");

        public static readonly int BusinessCodePos = 1;
        public static readonly int CourseCodePos = 6;


        public static string GetTitle(this Post post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            //Attention to successive dashes in WP. They are rendered as single unicode character (e.g. --- -> '\u2014')
            return HttpUtility.HtmlDecode(post.Title.Rendered);
        }
    }
}
