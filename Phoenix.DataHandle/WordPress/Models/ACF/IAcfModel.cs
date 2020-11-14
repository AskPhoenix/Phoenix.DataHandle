namespace Phoenix.DataHandle.WordPress.ACF
{
    public interface IAcfModel<CtxT>
    {
        abstract bool MatchesUnique(CtxT match);
        abstract CtxT ToContext();
        abstract IAcfModel<CtxT> WithTitleCase();
    }
}
