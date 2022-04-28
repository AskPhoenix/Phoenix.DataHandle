namespace Phoenix.DataHandle.DataEntry.Models.Uniques
{
    public class BusinessUnique
    {
        public int Code { get; }
        public BusinessType BusinessType { get; }
        
        public BusinessUnique(string postTitle)
        {
            if (string.IsNullOrEmpty(postTitle))
                throw new ArgumentNullException(nameof(postTitle));

            postTitle = postTitle.ToUpper();

            if (!PostExtensions.BusinessUQRgx.IsMatch(postTitle))
                throw new ArgumentException("Post title is not well formed.");

            this.BusinessType = (BusinessType)postTitle.First();
            this.Code = int.Parse(postTitle[PostExtensions.BusinessCodePos..]);
        }

        public BusinessUnique(int code, char businessChar)
        {
            this.Code = code;
            this.BusinessType = (BusinessType)businessChar;
        }

        public BusinessUnique(int code, BusinessType businessType)
            : this(code, (char)businessType)
        {
        }

        public override bool Equals(object? other)
        {
            return other is BusinessUnique unique &&
                   this.Code == unique.Code &&
                   this.BusinessType == unique.BusinessType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Code, this.BusinessType);
        }

        public override string ToString()
        {
            return "" + ((char)this.BusinessType) + this.Code;
        }
    }
}
