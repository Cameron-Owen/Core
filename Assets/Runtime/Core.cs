using System;
using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Mana
{

    /// <summary>
    /// Static class to provide a global interface for game loop events and Unity coroutines.
    /// </summary>
    public static class Core
    {
        static Action frameAction;
        static Action fixedStepAction;
        static Action lateFrameAction;

        public static event Action FrameUpdated
        {
            add { frameAction -= value; frameAction += value; }
            remove { frameAction -= value; }
        }

        public static event Action LateFrameUpdated
        {
            add { lateFrameAction -= value; lateFrameAction += value; }
            remove { lateFrameAction -= value; }
        }

        public static event Action FixedStepUpdated
        {
            add { fixedStepAction -= value; fixedStepAction += value; }
            remove { fixedStepAction -= value; }
        }

        const string OBJECT_NAME = "[Core]";
        static CoreBehaviour instance;

#if UNITY_EDITOR
        private static bool debug;
        public static bool Debug { get => debug; set => debug = value; }
#endif

        class CoreBehaviour : MonoBehaviour
        {

            void Awake()
            {
                DontDestroyOnLoad(this);
                hideFlags = HideFlags.NotEditable;
            }

            void Update()
            {
                frameAction?.Invoke();
            }

            void FixedUpdate()
            {
                fixedStepAction?.Invoke();
            }

            void LateUpdate()
            {
                lateFrameAction?.Invoke();
            }

            private void OnDestroy()
            {
                StopAllCoroutines();
            }

        }

        /// <summary>
        /// Static constructor initalizes Behaviour instance that 
        /// handles the how the class intergrates with Unity's game loop
        /// </summary>
        static Core()
        {
            GetBehaviour();
        }

        static CoreBehaviour GetBehaviour()
        {
            if (instance != null)
            {
                return instance;
            }

            instance = Object.FindObjectOfType<CoreBehaviour>();

            if (instance != null)
            {
                return instance;
            }

            var obj = new GameObject(OBJECT_NAME)
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            Object.DontDestroyOnLoad(obj);
            instance = obj.AddComponent<CoreBehaviour>();

            return instance;

        }

        /// <summary>
        /// Starts a coroutine
        /// </summary>
        /// <param name="routine">The enumerator method that will function as a coroutine</param>
        /// <returns>A coroutine handle that can be use to stop the coroutine</returns>
        public static Coroutine StartCoroutine(IEnumerator routine)
        {
            return GetBehaviour().StartCoroutine(routine);
        }

        /// <summary>
        /// Stops the coroutine associated with the given handle
        /// </summary>
        /// <param name="routine">The coroutine handle to identify the routine to be stopped</param>
        public static void StopCoroutine(Coroutine routine)
        {
            GetBehaviour().StopCoroutine(routine);
        }
    }
}