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
            if (string.IsNullOrEmpty(postTitle))
                throw new ArgumentNullException(nameof(postTitle));

            if (!postTitle.Contains(PostExtensions.SecondaryDelimiter))
                throw new ArgumentException("Post title is not well formed.");

            string[] uniqueParts = postTitle.Trim().
                Split(PostExtensions.PrimaryDelimiter, StringSplitOptions.RemoveEmptyEntries).
                First().
                Split(PostExtensions.SecondaryDelimiter, StringSplitOptions.RemoveEmptyEntries);

            this.NormalizedSchoolName = uniqueParts[0].ToUpperInvariant();
            this.NormalizedSchoolCity = uniqueParts[1].ToUpperInvariant();
        }

        public SchoolUnique(string schoolName, string schoolCity)
        {
            if (string.IsNullOrEmpty(schoolName))
                throw new ArgumentNullException(nameof(schoolName));
            if (string.IsNullOrEmpty(schoolCity))
                throw new ArgumentNullException(nameof(schoolCity));

            this.NormalizedSchoolName = schoolName.ToUpperInvariant();
            this.NormalizedSchoolCity = schoolCity.ToUpperInvariant();
        }

        public override bool Equals(object? other)
        {
            return other is SchoolUnique unique &&
                   NormalizedSchoolName == unique.NormalizedSchoolName &&
                   NormalizedSchoolCity == unique.NormalizedSchoolCity;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NormalizedSchoolName, NormalizedSchoolCity);
        }

        public override string ToString()
        {
            return NormalizedSchoolName + PostExtensions.SecondaryDelimiter + NormalizedSchoolCity;
        }
    }
}
