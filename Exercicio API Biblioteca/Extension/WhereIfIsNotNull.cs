using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercicio_API_Biblioteca.Extension
{
    public static class WhereNotNull
    {
        public static IEnumerable<TSource> WhereIfIsNotNull<TSource>(this IEnumerable<TSource> source, object para, Func<TSource, bool> predicate)
        {
            if (para == null) return source;
            return source.Where(predicate);
        }
    }
}
