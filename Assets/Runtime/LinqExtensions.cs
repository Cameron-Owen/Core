using System;
using System.Collections.Generic;
using JetBrains.Annotations;

public static class LinqExtensions
{
    [CanBeNull]
    public static T FirstOrNothing<T>(this IEnumerable<T> items, Func<T, bool> predicate) where T : class
    {
        var enumerator = items.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (current == null)
            {
                return null;
            }
            if (predicate(current))
            {
                return current;
            }
        }
        return null;
    }

    [CanBeNull]
    public static U DoElse<T, U>(this IEnumerable<T> items, Func<T, bool> predicate, Func<T, U> doAction, Func<U> elseAction) where T : class
    {
        var enumerator = items.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (enumerator.Current == null) { continue; }
            if (predicate(enumerator.Current))
            {
                
                return doAction == null ? default : doAction(enumerator.Current);
            }
        }
        return elseAction == null ? default : elseAction();
    }

}
