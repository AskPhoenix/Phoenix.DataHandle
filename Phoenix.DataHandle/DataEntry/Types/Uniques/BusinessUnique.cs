using Phoenix.DataHandle.DataEntry.Types;

namespace Phoenix.DataHandle.DataEntry.Types.Uniques
{
    public class BusinessUnique
    {
        public int Code { get; }
        public BusinessType BusinessType { get; }

        public BusinessUnique(string postTitle)
        {
            if (string.IsNullOrEmpty(postTitle))
                throw new ArgumentNullException(nameof(postTitle));

            string businessTitle = postTitle.ToUpper().Split('_').First();

            if (!PostExtensions.BusinessUQRgx.IsMatch(businessTitle))
                throw new ArgumentException("Post title is not well formed.");

            BusinessType = (BusinessType)businessTitle.First();
            Code = int.Parse(businessTitle[PostExtensions.BusinessCodePos..]);
        }

        public BusinessUnique(int code, char businessChar)
        {
            Code = code;
            BusinessType = (BusinessType)businessChar;
        }

        public BusinessUnique(int code, BusinessType businessType)
            : this(code, (char)businessType)
        {
        }

        public override bool Equals(object? other)
        {
            return other is BusinessUnique unique &&
                   Code == unique.Code &&
                   BusinessType == unique.BusinessType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Code, BusinessType);
        }

        public override string ToString()
        {
            return "" + (char)BusinessType + Code;
        }
    }
}
