using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Mana
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this MemberInfo memberInfo, bool inherit = false) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Length > 0;
        }

        [CanBeNull]
        public static T GetAttribute<T>(this MemberInfo memberInfo, bool inherit = false, uint index = 0) where T : Attribute
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (attributes.Length < 1)
            {
                return null;
            }

            return (T)attributes[index];
        }

        public static bool TryGetAttribute<T>(this MemberInfo memberInfo, [CanBeNull] out T attribute, bool inherit = false, uint index = 0) where T : Attribute
        {
            attribute = memberInfo.GetAttribute<T>(inherit, index);
            return attribute != null;
        }

        public static bool TryGetAttribute<T>(this MemberInfo memberInfo, Action<T> onSuccess, bool inherit = false, uint index = 0) where T : Attribute
        {
            var attribute = memberInfo.GetAttribute<T>(inherit, index);
            if (attribute == null)
            {
                return false;
            }

            onSuccess(attribute);
            return true;
        }
    }
}