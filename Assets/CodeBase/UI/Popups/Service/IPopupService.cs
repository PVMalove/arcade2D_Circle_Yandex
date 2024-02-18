using CodeBase.UI.Popups.Base;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public interface IPopupService
    {
        UniTask ShowPopup<TInitializeData>(PopupName key, TInitializeData initializeData);
        bool IsPopupActive(PopupName key);
    }
}