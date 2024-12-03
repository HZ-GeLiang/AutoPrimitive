using System.Collections;

namespace AutoPrimitive.Test.ExtensionMethods
{
    static class IDictionaryExtensions
    {
        public static Dictionary<TKey, TElement> ToDictionarySameKeyContinue<TSource, TKey, TElement>(
              this IEnumerable<TSource> source,
              Func<TSource, TKey> keySelector,
              Func<TSource, TElement> elementSelector
          ) where TKey : notnull
      => source.ToDictionarySameKeyContinue(keySelector, elementSelector, null);

        public static Dictionary<TKey, TElement> ToDictionarySameKeyContinue<TSource, TKey, TElement>(
                this IEnumerable<TSource> source,
                Func<TSource, TKey> keySelector,
                Func<TSource, TElement> elementSelector,
                IEqualityComparer<TKey> comparer
            ) where TKey : notnull
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }

            if (elementSelector == null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }

            int capacity = 0;
            if (source is ICollection<TSource> collection)
            {
                capacity = collection.Count;
                if (capacity == 0)
                {
                    return new Dictionary<TKey, TElement>(comparer);
                }

                if (collection is TSource[] array)
                {
                    return ToDictionarySameKeyContinue(array, keySelector, elementSelector, comparer);
                }

                if (collection is List<TSource> list)
                {
                    return ToDictionarySameKeyContinue(list, keySelector, elementSelector, comparer);
                }
            }

            Dictionary<TKey, TElement> d = new Dictionary<TKey, TElement>(capacity, comparer);
            foreach (TSource element in source)
            {
                var key = keySelector(element);
                if (d.ContainsKey(key))
                {
                    continue;
                }
                d.Add(key, elementSelector(element));
            }

            return d;
        }

    }
}