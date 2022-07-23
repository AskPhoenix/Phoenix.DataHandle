using Phoenix.DataHandle.DataEntry.Types;

namespace Phoenix.DataHandle.DataEntry.Models.Uniques
{
    public class PublisherUnique : BusinessUnique
    {
        public PublisherUnique(string postTitle)
            : base(postTitle)
        {
            if (!postTitle.ToUpper().StartsWith('P'))
                throw new InvalidOperationException("Publisher Unique must start with 'P'");
        }

        public PublisherUnique(int code)
            : base(code, BusinessType.Publisher)
        {
        }
    }
}
