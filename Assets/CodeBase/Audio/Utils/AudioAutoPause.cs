using CodeBase.Audio.Core;
using UnityEngine;

namespace CodeBase.Audio.Utils
{
    public class AudioAutoPause : MonoBehaviour
    {
        private bool _isFocused = true;

        private void OnEnable()
        {
            AppFocusHandle.OnFocus += Focus;
            AppFocusHandle.OnUnfocus += UnFocus;
        }

        private void OnDisable()
        {
            AppFocusHandle.OnFocus -= Focus;
            AppFocusHandle.OnUnfocus -= UnFocus;
        }

        private void Focus()
        {
            if (_isFocused == false)
            {
                AudioListener.pause = false;
                _isFocused = true;

                Debug.Log("Unpause Audio");
            }
        }

        private void UnFocus()
        {
            if (_isFocused)
            {
                AudioListener.pause = true;
                _isFocused = false;
                
                Debug.Log("Pause Audio");
            }
        }
    }
}