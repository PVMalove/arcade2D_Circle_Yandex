using UnityEngine;

namespace CodeBase.Core.Services.InputService
{
    public sealed class MobileInputService : IInputService
    {
        public bool GetInputClick()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.phase == TouchPhase.Began;
            }

            return false;
        }

        public bool GetInputSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                return touch.phase is TouchPhase.Moved or TouchPhase.Stationary;
            }

            return false;
        }
    }
}