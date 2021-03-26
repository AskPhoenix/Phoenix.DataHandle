using Phoenix.DataHandle.WordPress.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.Models.Uniques
{
    public class CourseUnique
    {
        public SchoolUnique SchoolUnique { get; }
        public short Code { get; }

        public CourseUnique(string postTitle)
        {
            this.SchoolUnique = new SchoolUnique(postTitle);

            string unique = postTitle?.
                Split(PostExtensions.PrimaryDelimiter, StringSplitOptions.RemoveEmptyEntries).
                LastOrDefault()?.
                Split(PostExtensions.SecondaryDelimiter, StringSplitOptions.RemoveEmptyEntries).
                LastOrDefault();

            bool codeParsed = short.TryParse(unique, out short code);
            if (!codeParsed)
                throw new InvalidOperationException($"The course code \"{unique}\" in the title of the post is not valid.");

            this.Code = code;
        }

        public CourseUnique(SchoolUnique schoolUnique, short code)
        {
            this.SchoolUnique = schoolUnique;
            this.Code = code;
        }
    }
}
