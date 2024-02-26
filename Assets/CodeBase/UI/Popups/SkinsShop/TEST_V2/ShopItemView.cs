using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Popups.SkinsShop.TEST_V2
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI requiredCoinsText;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image lockImage;
        [SerializeField] private Image selectLabel;
        
        public bool IsSelected => selectLabel.gameObject.activeSelf;

        public void SetItem(string name, Sprite image, int requiredCoins)
        {
            itemNameText.text = name;
            itemIcon.sprite = image;
            requiredCoinsText.text = requiredCoins.ToString();
        }
        
        public void Lock()
        {
            lockImage.gameObject.SetActive(true);
        }

        public void Unlock()
        {
            lockImage.gameObject.SetActive(false);
        }

        public void Select()
        {
            selectLabel.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            selectLabel.gameObject.SetActive(false);
        }
    }
}