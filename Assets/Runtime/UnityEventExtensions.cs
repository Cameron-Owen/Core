using System;
using UnityEngine.Events;

namespace Mana
{
    public static class UnityEventExtensions
    {
        /// <summary>
        /// Adds a single use listener that, once invoked, is removed from the event.
        /// You can also specify if other listeners should be removed prior to adding
        /// the given listener.
        /// </summary>
        /// <param name="unityEvent">The event to add the action to</param>
        /// <param name="listener">The listeber to add</param>
        /// <param name="removeOtherListeners">A flag indicating if other listeners should be removed before adding the new one.</param>
        public static void AddSingleUseListener(this UnityEvent unityEvent, Action listener, bool removeOtherListeners = false)
        {
            UnityAction wrapper = null;
            if (removeOtherListeners)
            {
                unityEvent.RemoveAllListeners();
            }
            wrapper = () =>
            {
                unityEvent.RemoveListener(wrapper);
                listener?.Invoke();
            };
            unityEvent.AddListener(wrapper);
        }

        /// <summary>
        /// Removes all listeners on this UnityEvent before adding the given listener.
        /// </summary>
        /// <param name="unityEvent">The event to add the action to</param>
        /// <param name="listener">The listeber to add</param>
        public static void AddSingleListener(this UnityEvent unityEvent, UnityAction listener)
        {
            unityEvent.RemoveAllListeners();
            unityEvent.AddListener(listener);
        }

        /// <summary>
        /// Removes all listeners on this UnityEvent before adding the given listener.
        /// </summary>
        /// <param name="unityEvent">The event to add the action to</param>
        /// <param name="listener">The listeber to add</param>
        public static void AddSingleListener<T>(this UnityEvent<T> unityEvent, UnityAction<T> listener)
        {
            unityEvent.RemoveAllListeners();
            unityEvent.AddListener(listener);
        }

        /// <summary>
        /// Adds a single use listener that, once invoked, is removed from the event.
        /// You can also specify if other listeners should be removed prior to adding
        /// the given listener.
        /// </summary>
        /// <param name="unityEvent">The event to add the action to</param>
        /// <param name="listener">The listeber to add</param>
        /// <param name="removeOtherListeners">A flag indicating if other listeners should be removed before adding the new one.</param>
        public static void AddSingleUseListener<T>(this UnityEvent<T> unityEvent, Action<T> listener, bool removeOtherListeners = false)
        {
            UnityAction<T> wrapper = null;
            if (removeOtherListeners)
            {
                unityEvent.RemoveAllListeners();
            }

            wrapper = (T value) =>
            {
                unityEvent.RemoveListener(wrapper);
                listener?.Invoke(value);
            };
            unityEvent.AddListener(wrapper);
        }
    }
}