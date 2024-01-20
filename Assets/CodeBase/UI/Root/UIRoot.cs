using UnityEngine;
using Zenject;

namespace CodeBase.UI.Root
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        [field: SerializeField] public Transform ContainerWindow { get; private set; }
        [field: SerializeField] public Transform ContainerHUD { get; private set; }
        [field: SerializeField] public Transform ContainerPopup { get; private set; }
        
        public class Factory : PlaceholderFactory<UIRoot> { }       
    }
}