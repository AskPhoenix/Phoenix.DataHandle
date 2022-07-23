namespace Phoenix.DataHandle.DataEntry.Types.Uniques
{
    public class CourseUnique
    {
        public SchoolUnique SchoolUnique { get; }
        public short Code { get; }

        public CourseUnique(string postTitle)
        {
            if (string.IsNullOrEmpty(postTitle))
                throw new ArgumentNullException(nameof(postTitle));

            if (!PostExtensions.CourseUQRgx.IsMatch(postTitle))
                throw new ArgumentException("Post title is not well formed.");

            string[] uqParts = postTitle.Split('_');

            SchoolUnique = new(uqParts[0]);
            Code = short.Parse(uqParts[1][PostExtensions.CourseCodePos..]);
        }

        public CourseUnique(SchoolUnique schoolUnique, short code)
        {
            SchoolUnique = schoolUnique;
            Code = code;
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
            return SchoolUnique.ToString() + "_Course-" + Code;
        }
    }
}
