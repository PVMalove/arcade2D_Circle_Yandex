using CodeBase.UI.Popups.SkinsShop.TEST.SkinsShop;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public class SkinItemView : MonoBehaviour
    {
        [SerializeField] private Image skinImage;
        [SerializeField] private Image lockImage;
        
        public ShopItemConfig Item { get; private set; }
        public bool IsLock { get; private set; }

        public void SetSkin(Sprite image)
        {
            skinImage.sprite = image;
        }
        
        public void Initialize(ShopItemConfig item)
        {
            Item = item;
        }
        
        public void Lock()
        {
            IsLock = true;
            lockImage.gameObject.SetActive(IsLock);
        }

        public void Unlock()
        {
            IsLock = false;
            lockImage.gameObject.SetActive(IsLock);
        }
    }
}