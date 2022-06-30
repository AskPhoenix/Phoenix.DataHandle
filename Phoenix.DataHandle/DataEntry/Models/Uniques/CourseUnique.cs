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

            if (!PostExtensions.CourseUQRgx.IsMatch(postTitle))
                throw new ArgumentException("Post title is not well formed.");

            string[] uqParts = postTitle.Split('_');

            this.SchoolUnique = new(uqParts[0]);
            this.Code = short.Parse(uqParts[1][PostExtensions.CourseCodePos..]);
        }

        public CourseUnique(SchoolUnique schoolUnique, short code)
        {
            this.SchoolUnique = schoolUnique;
            this.Code = code;
        }

        public override bool Equals(object? other)
        {
            return other is CourseUnique courseUnique &&
                   this.SchoolUnique.Equals(courseUnique.SchoolUnique) &&
                   this.Code == courseUnique.Code;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.SchoolUnique, this.Code);
        }

        public override string ToString()
        {
            return this.SchoolUnique.ToString() + "_Course-" + Code;
        }
    }
}
