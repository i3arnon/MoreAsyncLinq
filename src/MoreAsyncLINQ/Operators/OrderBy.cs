﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MoreAsyncLINQ.OrderByDirection;

namespace MoreAsyncLINQ
{
    static partial class MoreAsyncEnumerable
    {
        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            OrderByDirection direction)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));
            
            return source.OrderBy(keySelector,comparer: null, direction);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderBy<TSource, TKey>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, TKey> keySelector,
            IComparer<TKey>? comparer,
            OrderByDirection direction)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));

            comparer ??= Comparer<TKey>.Default;
            return direction == Ascending
                ? source.OrderBy(keySelector, comparer)
                : source.OrderByDescending(keySelector, comparer);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, ValueTask<TKey>> keySelector,
            OrderByDirection direction)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));

            return source.OrderByAwait(keySelector, comparer: null, direction);
        }

        public static IOrderedAsyncEnumerable<TSource> OrderByAwait<TSource, TKey>(
            this IAsyncEnumerable<TSource> source,
            Func<TSource, ValueTask<TKey>> keySelector,
            IComparer<TKey>? comparer,
            OrderByDirection direction)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (keySelector is null) throw new ArgumentNullException(nameof(keySelector));

            comparer ??= Comparer<TKey>.Default;
            return direction == Ascending
                ? source.OrderByAwait(keySelector, comparer)
                : source.OrderByDescendingAwait(keySelector, comparer);
        }
    }
}