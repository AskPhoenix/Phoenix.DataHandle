using Phoenix.DataHandle.Main.Models.Extensions;
using Phoenix.DataHandle.WordPress.Models.Uniques;
using System;
using System.Linq.Expressions;

namespace Phoenix.DataHandle.WordPress.Models
{
    public interface IModelACF<CtxT> where CtxT : IModelEntity
    {
        SchoolUnique SchoolUnique { get; set; }

        abstract Expression<Func<CtxT, bool>> MatchesUnique { get; }
        //TODO: Allow for optional parameters in order to pass values like the foreign keys
        abstract CtxT ToContext();      //The context object returned does NOT include any of the foreign keys
        abstract IModelACF<CtxT> WithTitleCase();
    }
}
