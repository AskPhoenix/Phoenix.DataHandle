using Phoenix.DataHandle.WordPress.Utilities;
using System;
using System.Linq;

namespace Phoenix.DataHandle.WordPress.Models.Uniques
{
    public class SchoolUnique
    {
        public string NormalizedSchoolName { get; }
        public string NormalizedSchoolCity { get; }

        public SchoolUnique(string postTitle)
        {
            string[] uniqueParts = postTitle?.
                Split(PostExtensions.PrimaryDelimiter).
                FirstOrDefault()?.
                Split(PostExtensions.SecondaryDelimiter);

            if (uniqueParts is null || uniqueParts.Length != 2 || uniqueParts.Any(u => string.IsNullOrWhiteSpace(u)))
                throw new InvalidOperationException("The title of the post is not valid.");

            //School unique is already in capital in the title
            this.NormalizedSchoolName = uniqueParts[0].ToUpperInvariant();
            this.NormalizedSchoolCity = uniqueParts[1].ToUpperInvariant();
        }

        public SchoolUnique(string schoolName, string schoolCity)
        {
            //School unique is already in capital in the title
            this.NormalizedSchoolName = schoolName?.ToUpperInvariant();
            this.NormalizedSchoolCity = schoolCity?.ToUpperInvariant();
        }
    }
}
