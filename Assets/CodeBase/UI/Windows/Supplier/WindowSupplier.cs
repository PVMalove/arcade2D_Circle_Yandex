using System;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;
using CodeBase.UI.Windows.Base;

namespace CodeBase.UI.Windows.Supplier
{
    public class WindowSupplier : FrameSupplier<ScreenName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public WindowSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(ScreenName key)
        {
            switch (key)
            {
                case ScreenName.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}