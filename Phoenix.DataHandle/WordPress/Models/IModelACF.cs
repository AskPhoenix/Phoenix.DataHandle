using System;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public interface IModelACF<CtxT>
    {
        abstract Expression<Func<CtxT, bool>> MatchesUnique { get; }
        abstract CtxT ToContext();
        abstract IModelACF<CtxT> WithTitleCase();
    }
}
