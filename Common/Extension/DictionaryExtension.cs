using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Gdot.Care.Common.Extension
{
    [ExcludeFromCodeCoverage]
    public static class DictionaryExtension
    {
        public static IDictionary<TKey, TVal> Merge<TKey, TVal>(this IDictionary<TKey, TVal> dictA,
            IDictionary<TKey, TVal> dictB)
        {
            foreach (var pair in dictB)
            {
                if (!dictA.ContainsKey(pair.Key))
                {
                    dictA.Add(pair.Key, pair.Value);
                }
            }
            return dictA;
        }
    }
}
