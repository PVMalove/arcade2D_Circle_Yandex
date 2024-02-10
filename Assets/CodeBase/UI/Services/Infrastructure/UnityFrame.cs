using UnityEngine;

namespace CodeBase.UI.Services.Infrastructure
{
    public abstract class UnityFrame : MonoBehaviour
    {
        public abstract void OnShow(object args);
        public abstract void OnHide();
    }
}