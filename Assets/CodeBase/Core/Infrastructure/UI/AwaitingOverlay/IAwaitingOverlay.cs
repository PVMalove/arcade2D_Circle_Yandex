namespace CodeBase.Core.Infrastructure.UI.AwaitingOverlay
{
    public interface IAwaitingOverlay
    {
        void Show(string withMessage);
        void Hide();
    }
}