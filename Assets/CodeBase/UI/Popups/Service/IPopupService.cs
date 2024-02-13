using CodeBase.UI.Popups.Base;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Popups.Service
{
    public interface IPopupService
    {
        void ShowPopup<TInitializeData, TResult>(PopupName key, TInitializeData initializeData);
        bool IsPopupActive(PopupName key);
    }
}