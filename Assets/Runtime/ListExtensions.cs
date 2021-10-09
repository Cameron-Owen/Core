using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mana
{
    public static class ListExtensions
    {
        const string ExceptionMsgAccessItemEmptyList = "Attept to access item of an empty list";
        const string ExceptionMsgRemoveItemEmptyList = "Attept to remove an item from an empty list";

        static readonly System.Random random = new System.Random();

        /// <summary>
        /// Returns the last element in the list.
        /// </summary>
        [CanBeNull]
        public static T Last<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            return list[list.Count - 1];
        }

        /// <summary>
        /// Returns the first element in the list.
        /// </summary>
        [CanBeNull]
        public static T First<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            return list[0];
        }

        /// <summary>
        /// Removes and returns the first item in the list.
        /// </summary>
        [CanBeNull]
        public static T PopFirst<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgRemoveItemEmptyList);
            var item = list[0];
            list.RemoveAt(0);
            return item;
        }

        /// <summary>
        /// Adds an item at the start of the list.
        /// </summary>
        public static void PushFirst<T>(this IList<T> list, T item)
        {
            list.Insert(0, item);
        }

        /// <summary>
        /// Removes and returns the last item in the list.
        /// </summary>
        [CanBeNull]
        public static T PopLast<T>(this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgRemoveItemEmptyList);
            var element = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return element;
        }

        /// <summary>
        /// Adds an item at the end of the list.
        /// </summary>
        public static void PushLast<T>(this IList<T> list, T item)
        {
            list.Add(item);
        }

        /// <summary>
        /// Returns a random element from the list
        /// </summary>
        [CanBeNull]
        public static T Random<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            var randomInt = UnityEngine.Random.Range(0, list.Count);
            return list[randomInt];
        }

        /// <summary>
        /// Returns a random element from the list using the given random state.
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This version is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [CanBeNull]
        public static T Random<T>([NotNull] this IList<T> list, Random.State state)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            var prevState = UnityEngine.Random.state;
            UnityEngine.Random.state = state;
            var result = Random(list);
            UnityEngine.Random.state = prevState;
            return result;
        }

        /// <summary>
        /// Returns a random element from the list using the given random instance.
        /// </summary>
        [CanBeNull]
        public static T Random<T>([NotNull] this IList<T> list, System.Random random)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            return list[random.Next(0, list.Count)];
        }

        /// <summary>
        /// Returns a random element from the list using an internal random instance.
        /// 
        /// This version is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [CanBeNull]
        public static T RandomStateless<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgAccessItemEmptyList);
            return list[random.Next(0, list.Count)];
        }

        /// <summary>
        /// Removes and returns a random element from the list. 
        /// </summary>
        [CanBeNull]
        public static T PopRandom<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgRemoveItemEmptyList);
            var randomInt = UnityEngine.Random.Range(0, list.Count);
            var item = list[randomInt];
            list.RemoveAt(randomInt);
            return item;
        }

        /// <summary>
        /// Removes and returns a random element from the list using the
        /// given random instance. 
        /// </summary>
        [CanBeNull]
        public static T PopRandom<T>([NotNull] this IList<T> list, System.Random random)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgRemoveItemEmptyList);
            var randomInt = random.Next(0, list.Count);
            var item = list[randomInt];
            list.RemoveAt(randomInt);
            return item;
        }

        /// <summary>
        /// Removes and returns a random element from the list using an internal
        /// random instance.
        /// 
        /// This version is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [CanBeNull]
        public static T PopRandomStateless<T>([NotNull] this IList<T> list)
        {
            Assert.IsTrue(list.Count > 0, ExceptionMsgRemoveItemEmptyList);
            var randomInt = random.Next(0, list.Count);
            var item = list[randomInt];
            list.RemoveAt(randomInt);
            return item;
        }

        /// <summary>
        /// Creates a copy of the current list then shuffles and returns the copy. 
        /// The order of the current list is not changed. This uses the modern 
        /// form of a Fisher-Yates alrogithm which is of O(n) complexity.
        /// </summary>
        [ItemCanBeNull]
        public static List<T> Shuffle<T>(this List<T> list)
        {
            list = new List<T>(list);
            FisherYatesShuffleUnityRandom(list);
            return list;
        }

        /// <summary> 
        /// Creates a copy of the list then shuffles it the given seed to randomize
        /// the order of elements. This uses the modern form of a Fisher-Yates 
        /// alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static List<T> Shuffle<T>([NotNull] this List<T> list, int seed)
        {
            list = new List<T>(list);
            ShuffleInPlace(list, seed);
            return list;
        }

        /// <summary> 
        /// Creates a copy of the list then shuffles it the given state to randomize
        /// the order of elements. This uses the modern form of a Fisher-Yates 
        /// alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static List<T> Shuffle<T>([NotNull] this List<T> list, Random.State state)
        {
            list = new List<T>(list);
            ShuffleInPlace(list, state);
            return list;
        }

        /// <summary> 
        /// Creates a copy of the list then shuffles it  using an internal random 
        /// instance to randomize the order of elements. This uses the modern 
        /// form of a Fisher-Yates alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static List<T> ShuffleStateless<T>([NotNull] this List<T> list)
        {
            list = new List<T>(list);
            FisherYatesShuffle(list, random);
            return list;
        }

        /// <summary>
        /// Shuffles the list in place randomising the order of elements. 
        /// This uses the modern form of a Fisher-Yates alrogithm which is
        /// of O(n) complexity.
        /// </summary>
        [ItemCanBeNull]
        public static void ShuffleInPlace<T>([NotNull] this IList<T> list)
        {
            FisherYatesShuffleUnityRandom(list);
        }

        /// <summary>
        /// Shuffles the list in place using the given random seed to randomize 
        /// the order of elements. This uses the modern form of a Fisher-Yates 
        /// alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static void ShuffleInPlace<T>([NotNull] this IList<T> list, int seed)
        {
            var prevState = UnityEngine.Random.state;
            UnityEngine.Random.InitState(seed);
            FisherYatesShuffleUnityRandom(list);
            UnityEngine.Random.state = prevState;
        }

        /// <summary>
        /// Shuffles the list in place using the given random state to randomize 
        /// the order of elements. This uses the modern form of a Fisher-Yates 
        /// alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will be restored afterwards.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static void ShuffleInPlace<T>(this IList<T> list, Random.State state)
        {
            var prevState = UnityEngine.Random.state;
            UnityEngine.Random.state = state;
            FisherYatesShuffleUnityRandom(list);
            UnityEngine.Random.state = prevState;
        }


        /// <summary>
        /// Shuffles the list in place using an internal random instance randomize 
        /// the order of elements. This uses the modern form of a Fisher-Yates 
        /// alrogithm which is of O(n) complexity. 
        /// 
        /// Unity's internal random state will not be altered by this method.
        /// 
        /// This overload is helpful if you need to preserve Unity's static
        /// random state. 
        /// </summary>
        [ItemCanBeNull]
        public static void ShuffleInPlaceStateless<T>([NotNull] this IList<T> list)
        {
            FisherYatesShuffle(list, random);
        }

        /// <summary>
        /// Returns the next item in the list, wrapping to the first item 
        /// once it reached the end.
        /// </summary>
        /// <param name="index">The index of the item previous to next</param>
        [CanBeNull]
        public static T NextWrapped<T>(this IList<T> list, int index)
        {
            var nextIndex = (index == list.Count - 1) ? 0 : index + 1;
            return list[nextIndex];
        }

        /// <summary>
        /// Returns the previous item in the list, wrapping to the last item 
        /// once it reached the start.
        /// </summary>
        /// <param name="index">The index of the item next to previous</param>
        [CanBeNull]
        public static T PrevWrapped<T>(this IList<T> list, int index)
        {
            var prevIndex = (index == 0) ? list.Count - 1 : index - 1;
            return list[prevIndex];
        }

        /// <summary>
        /// Adds the given item to the list if the item is not null.
        /// </summary>
        /// <param name="value">The value to add</param>
        public static void AddIfNotNull<T>(this List<T> list, [CanBeNull] T value)
        {
            if (value == null)
            {
                return;
            }
            list.Add(value);
        }

        /// <summary>
        /// Adds all items of the given collection to the list, skipping items that are null. 
        /// </summary>
        /// <param name="values">The collection to add</param>
        public static void AddIfNotNull<T>(this List<T> list, IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                if (value == null)
                {
                    return;
                }
                list.Add(value);
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the IEnumerable<T>.
        /// </summary>
        /// <param name="action">The action to perform</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, System.Action<T> action)
        {
            foreach(var item in enumerable)
            {
                action?.Invoke(item);
            }
        }

        [ItemCanBeNull]
        static void FisherYatesShuffleUnityRandom<T>(IList<T> list)
        {
            var count = list.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = UnityEngine.Random.Range(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }

        [ItemCanBeNull]
        static void FisherYatesShuffle<T>(IList<T> list, System.Random random)
        {
            var count = list.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var r = random.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }
    }
}