using CodeBase.Core.Infrastructure.AssetManagement;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class CircleHero : MonoBehaviour
    {
        [SerializeField] private Transform containerView;
        
        private GameObject prefabView;

        public void SetView(GameObject view)
        {
            Clear();
            prefabView = view;
            prefabView.transform.SetParent(containerView, false);
        }

        private void Clear()
        {
            if (prefabView != null)
                Destroy(prefabView);
        }

        public class Factory : AddressablePrefabFactory<CircleHero> { }
    }
}