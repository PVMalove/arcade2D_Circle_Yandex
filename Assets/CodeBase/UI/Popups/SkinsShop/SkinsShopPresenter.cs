using Zenject;

namespace CodeBase.UI.Popups.SkinsShop
{
    public sealed class SkinsShopPresenter : ISkinsShopPresenter
    {
        public sealed class Factory : PlaceholderFactory<ISkinsShopPresenter>
        {
        }
    }
}