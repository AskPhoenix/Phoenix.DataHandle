using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordPressPCL.Client;
using WordPressPCL.Models;

namespace Phoenix.WordPress.Puller.Extensions
{
    internal static class CustomRequestExtensions
    {
        internal static async Task<IEnumerable<T>> GetAll<T>(this CustomRequest cReq, string route, bool embed = false, bool useAuth = false)
            where T : class
        {
            //There is no access to the response headers of WordPress, like the X-WP-TotalPages,
            //so instead try to get elements until a response has less elements than the max per
            //page (100). If the last page has exactly 100 elements, then an exception is caught.
            
            route.Trim('/');

            var entities = new List<T>();
            char sep = route.Contains('?') ? '&' : '?';
            int page = 1;
            int oldCount;

            do
            {
                oldCount = entities.Count;
                entities.AddRange(await cReq.Get<IEnumerable<T>>($"{route}{sep}per_page=100&page={page++}", embed, useAuth).ConfigureAwait(false));
            } while (entities.Count % 100 == 0 && entities.Count != oldCount);

            return entities;
        }
    }
}
