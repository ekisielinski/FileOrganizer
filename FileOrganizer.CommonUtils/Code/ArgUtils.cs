using System;
using System.Collections.Generic;

namespace FileOrganizer.CommonUtils
{
    public static class ArgUtils
    {
        public static IReadOnlyList<T> ToRoList<T>( IEnumerable<T> items, string? paramName = null ) where T : class
        {
            paramName ??= nameof( items );

            Guard.NotNull( items, paramName );

            var result = new List<T>();

            foreach (T item in items)
            {
                if (item is null) throw new ArgumentException( "Given enumerable contains null element.", paramName );

                result.Add( item );
            }

            return result;
        }
    }
}
