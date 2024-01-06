using CodeBase.Core.Services.SaveLoadService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private Button buttonSaveProgress;

        private ISaveService saveService;
        
        [Inject]
        public void Construct(ISaveService saveService)   
        {
            this.saveService = saveService;
        }

        private void Start()
        {
            buttonSaveProgress.onClick.AddListener(SaveProgress);
        }

        private void OnDestroy()
        {
            buttonSaveProgress.onClick.RemoveListener(SaveProgress);
        }

        private void SaveProgress()
        {
            saveService.SaveProgress();
            Debug.Log("Progress Saved!");
        }
    }
}