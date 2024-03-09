using System;
using CodeBase.Core.Services.ProgressService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Popups.Shop.Item
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI itemNameText;
        [SerializeField] private TextMeshProUGUI requiredCoinsText;
        [SerializeField] private Image itemIcon;
        [SerializeField] private Image lockImage;
        [SerializeField] private Image selectLabel;
        
        [SerializeField] private Button selectButton;
        [SerializeField] private Button buyButton;
        
        public bool IsSelected => selectLabel.gameObject.activeSelf;

        private IPersistentProgressService progressService;
        private int requiredCoinsAmount;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService) => 
            this.progressService = progressService;

        private void OnEnable()
        {
            progressService.CoinsAmountChanged += OnPlayerProgressChanged;
        }

        private void OnDisable()
        {
            buyButton.onClick.RemoveAllListeners();
            selectButton.onClick.RemoveAllListeners();
            progressService.CoinsAmountChanged -= OnPlayerProgressChanged;
        }

        public void SetItem(string name, Sprite image, int requiredCoins,
            Action onBuyButtonClicked, Action onSelectButtonClicked)
        {
            itemNameText.text = name;
            itemIcon.sprite = image;
            requiredCoinsText.text = requiredCoins.ToString();
            
            buyButton.onClick.AddListener(() =>
            {
                onBuyButtonClicked?.Invoke();
                Unlock();
            });
            
            selectButton.onClick.AddListener(() =>
            {
                onSelectButtonClicked?.Invoke();
                Select();
            });

            requiredCoinsAmount = requiredCoins;
            OnPlayerProgressChanged();
        }

        public void Lock()
        {
            lockImage.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            selectButton.gameObject.SetActive(false);
        }

        public void Unlock()
        {
            lockImage.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
        }

        public void Select()
        {
            selectButton.gameObject.SetActive(false);
            selectLabel.gameObject.SetActive(true);
        }

        public void Unselect()
        {
            selectButton.gameObject.SetActive(true);
            selectLabel.gameObject.SetActive(false);
        }
        
        private void OnPlayerProgressChanged()
        {
            bool isCoinsEnough = progressService.IsCoinsEnoughFor(requiredCoinsAmount);
            buyButton.interactable = isCoinsEnough;
        }
    }
}