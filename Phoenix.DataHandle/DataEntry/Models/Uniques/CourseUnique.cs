using System;
using System.Linq;

namespace Phoenix.DataHandle.DataEntry.Models.Uniques
{
    public class CourseUnique
    {
        public SchoolUnique SchoolUnique { get; }
        public short Code { get; }

        public CourseUnique(string postTitle)
        {
            if (string.IsNullOrEmpty(postTitle))
                throw new ArgumentNullException(nameof(postTitle));

            bool titleOk = postTitle.Contains(PostExtensions.PrimaryDelimiter)
                && postTitle.Contains(PostExtensions.SecondaryDelimiter);
            if (!titleOk)
                throw new ArgumentException("Post title is not well formed.");

            this.SchoolUnique = new(postTitle);

            string codeStr = postTitle.Trim().
                Split(PostExtensions.PrimaryDelimiter, StringSplitOptions.RemoveEmptyEntries).
                Last().
                Split(PostExtensions.SecondaryDelimiter, StringSplitOptions.RemoveEmptyEntries).
                Last();

            bool codeParsed = short.TryParse(codeStr, out short code);
            if (!codeParsed)
                throw new InvalidOperationException($"The course code \"{codeStr}\" in the title of the post is not valid.");

            this.Code = code;
        }

        public CourseUnique(SchoolUnique schoolUnique, short code)
        {
            this.SchoolUnique = schoolUnique;
            this.Code = code;
        }

        public override bool Equals(object? other)
        {
            return other is CourseUnique courseUnique &&
                   SchoolUnique.Equals(courseUnique.SchoolUnique) &&
                   Code == courseUnique.Code;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SchoolUnique, Code);
        }

        public override string ToString()
        {
            return SchoolUnique.ToString() + PostExtensions.PrimaryDelimiter +
                "Course" + PostExtensions.SecondaryDelimiter + Code;
        }
    }
}
