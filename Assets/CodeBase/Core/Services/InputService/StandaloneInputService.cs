namespace CodeBase.Core.Services.InputService
{
    public sealed class StandaloneInputService : IInputService
    {
        public bool GetInputClick()
        {
            return UnityEngine.Input.GetMouseButtonDown(0);
        }

        public bool GetInputSwipe()
        {
            return UnityEngine.Input.GetMouseButton(0);
        }
    }
}