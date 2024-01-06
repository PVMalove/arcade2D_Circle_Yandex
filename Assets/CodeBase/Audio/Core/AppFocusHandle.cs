using System;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

namespace CodeBase.Audio.Core
{
    public static class AppFocusHandle
    {
        public static event Action OnFocus;
        public static event Action OnUnfocus;
        
        [DllImport("__Internal")]
        private static extern void FocusAppHandleInit(Action focus, Action unFocus);

        private static bool isInitialize = false;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            if (isInitialize)
                return;

            isInitialize = true;

            if (Application.isEditor || Application.platform != RuntimePlatform.WebGLPlayer)
                return;

            FocusAppHandleInit(Focus, UnFocus);
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void Focus()
        {
            OnFocus?.Invoke();
        }

        [MonoPInvokeCallback(typeof(Action))]
        private static void UnFocus()
        {
            OnUnfocus?.Invoke();
        }
    }
}