using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop.TEST
{
    public class SkinItemView : MonoBehaviour
    {
        [SerializeField] private Image skinImage;

        public void SetSkin(Sprite image)
        {
            skinImage.sprite = image;
        }
    }
}