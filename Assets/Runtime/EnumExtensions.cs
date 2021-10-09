using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Mana
{

    public static class EnumExtensions
    {

        /// <summary>
        /// Check to see if a flags enum value has a specific flag set.
        /// </summary>
        /// <param name="self">Flags enumeration to check</param>
        /// <param name="flag">Flag to check for</param>
        public static bool HasFlag<T>(this T self, T flag) where T : struct, IFormattable, IConvertible, IComparable
        {
            Assert.IsTrue(IsEnum<T>(true));
            var selfAsLong = Convert.ToInt64(self);
            var flagAsLong = Convert.ToInt64(flag);
            return (selfAsLong & flagAsLong) != 0;
        }

        /// <summary>
        /// Returns an enumeration of all the flags this value represents.
        /// </summary>
        /// <returns>An enumeration of all the flags this value represents</returns>
        public static IEnumerable<T> GetFlags<T>(this T self) where T : struct, IFormattable, IConvertible, IComparable
        {
            Assert.IsTrue(IsEnum<T>(true));
            foreach (var flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (self.HasFlag(flag))
                {
                    yield return flag;
                }
            }
        }

        /// <summary>
        /// Adds the given flags to the value. 
        /// </summary>
        /// <param name="flags">The flags to add</param>
        public static void SetFlags<T>(ref this T self, T flags) where T : struct, IFormattable, IConvertible, IComparable
        {
            Assert.IsTrue(IsEnum<T>(true));
            self = (T)Enum.ToObject(typeof(T), Convert.ToInt64(self) | Convert.ToInt64(flags));
        }

        /// <summary>
        /// Removes the given flags from the value. 
        /// </summary>
        /// <param name="flags">The flags to remove</param>
        public static void ClearFlags<T>(ref this T self, T flags) where T : struct, IFormattable, IConvertible, IComparable
        {
            Assert.IsTrue(IsEnum<T>(true));
            self = (T)Enum.ToObject(typeof(T), Convert.ToInt64(self) & ~Convert.ToInt64(flags));
        }


        /// <summary>
        /// Returns a value representing all the flags of the values in the given enumerable. 
        /// </summary>
        /// <param name="flags"></param>
        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : struct, IFormattable, IConvertible, IComparable
        {
            Assert.IsTrue(IsEnum<T>(true));
            var lValue = flags.Select(flag => Convert.ToInt64(flag))
                              .Aggregate<long, long>(0, (current, lFlag) => current | lFlag);
            return (T)Enum.ToObject(typeof(T), lValue);
        }

        /// <summary>
        /// Returns an enum of the specific generic type.
        /// </summary>
        /// <typeparam name="T">The type of the enum value</typeparam>
        /// <param name="value">The name of the enum value as a string</param>
        public static T AsEnum<T>(this string value) where T : struct, IFormattable, IConvertible, IComparable
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Returns an enum of the specific generic type. If that value is not 
        /// able to be parsed the given generic value is return instead. 
        /// </summary>
        /// <typeparam name="T">The type of the enum value</typeparam>
        /// <param name="value">The name of the enum value as a string</param>
        public static T AsEnumDefault<T>(this string value, T defaultValue) where T : struct, IFormattable, IConvertible, IComparable
        {
            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Return true if the string represents a valid value of the given Enum type.
        /// </summary>
        /// <param name="value">The enum to test against</param>
        /// <param name="comparison">The string comparison mode to use when matching the enum value's name.</param>
        /// <returns></returns>
        public static bool IsEnumValue(this string self, Enum value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            return value.ToString().Equals(self, comparison);
        }

        static bool IsEnum<T>(bool withFlags)
        {
            return typeof(T).IsEnum && (!withFlags || Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)));
        }

      
    }

}