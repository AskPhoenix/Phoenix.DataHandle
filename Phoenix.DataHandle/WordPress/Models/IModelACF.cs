namespace Phoenix.DataHandle.WordPress.Models
{
    public interface IModelACF<CtxT>
    {
        abstract bool MatchesUnique(CtxT match);
        abstract CtxT ToContext();
        abstract IModelACF<CtxT> WithTitleCase();
    }
}
