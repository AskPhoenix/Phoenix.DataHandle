using Phoenix.DataHandle.DataEntry.Types;

namespace Phoenix.DataHandle.DataEntry.Types.Uniques
{
    public class SchoolUnique : BusinessUnique
    {
        public SchoolUnique(string postTitle)
            : base(postTitle)
        {
            if (!postTitle.ToUpper().StartsWith('S'))
                throw new InvalidOperationException("School Unique must start with 'S'");
        }

        public SchoolUnique(int code)
            : base(code, BusinessType.School)
        {
        }
    }
}
