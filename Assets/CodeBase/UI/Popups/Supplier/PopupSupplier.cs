using System;
using CodeBase.UI.Popups.Base;
using CodeBase.UI.Services.Factories;
using CodeBase.UI.Services.Infrastructure;

namespace CodeBase.UI.Popups.Supplier
{
    public sealed class PopupSupplier : FrameSupplier<PopupName, UnityFrame>
    {
        private readonly IUIFactory uiFactory;

        public PopupSupplier(IUIFactory uiFactory)
        {
            this.uiFactory = uiFactory;
        }

        protected override UnityFrame InstantiateFrame(PopupName key)
        {
            switch (key)
            {
                case PopupName.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
            throw new InvalidOperationException($"Invalid key: {key}");
        }
    }
}