using UnityEngine;

namespace CodeBase.UI.Root
{
    public interface IUIRoot
    {
        Transform ContainerPopup { get; }
        Transform ContainerHUD { get; }
        Transform ContainerScreen { get; }
    }
}