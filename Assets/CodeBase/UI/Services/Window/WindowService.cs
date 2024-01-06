using System;
using CodeBase.UI.Root;
using CodeBase.UI.Services.Factories;

namespace CodeBase.UI.Services.Window
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory uiFactory;
        private IUIRoot viewport;
        
        public WindowService(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }
        
        public void InitializeRoot(IUIRoot viewport)
        {
            this.viewport = viewport;
        }

        public void Open(WindowId window)
        {
            switch (window)
            {
                case WindowId.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(window), window, null);
            }
        }
    }
}