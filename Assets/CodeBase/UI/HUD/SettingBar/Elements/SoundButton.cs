using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.UI.HUD.SettingBar.Elements
{
    public sealed class SoundButton : MonoBehaviour
    {
        [SerializeField] private Button button;

        [Space] 
        [SerializeField] private GameObject soundOn;
        [SerializeField] private GameObject soundOff;

        public void ChangeStateSoundButton(bool isOn)
        {
            soundOn.SetActive(isOn);
            soundOff.SetActive(!isOn);
        }

        public void AddListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }

        public void RemoveListener(UnityAction action)
        {
            button.onClick.RemoveListener(action);
        }
    }
}