using System;
using System.Collections.Generic;

namespace Infra.Util
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// This method iterate two IEnumerable at the same sequence.
        /// Source: https://stackoverflow.com/questions/2721939/how-to-iterate-through-two-ienumerables-simultaneously
        /// </summary>
        /// <param name="collection1"></param>
        /// <param name="collection2"></param>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<ZipEntry<T1, T2>> Zip<T1, T2>(
            this IEnumerable<T1> collection1, IEnumerable<T2> collection2)
        {
            if (collection1 == null)
                throw new ArgumentNullException(nameof(collection1));
            if (collection2 == null)
                throw new ArgumentNullException(nameof(collection2));

            var index = 0;
            using var enumerator1 = collection1.GetEnumerator();
            using var enumerator2 = collection2.GetEnumerator();
            
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                yield return new ZipEntry<T1, T2>(
                    index, enumerator1.Current, enumerator2.Current);
                index++;
            }
        }
    }
}