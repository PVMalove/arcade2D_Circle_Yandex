using System;
using UnityEngine;
using Zenject;

namespace CodeBase.Core.Services.SaveLoadService
{
    public class AutoSaveData : MonoBehaviour
    {
        private ISaveService saveService;

        [Inject]
        public void Construct(ISaveService saveLoadService)
        {
            saveService = saveLoadService;
        }

        public void SaveData()
        {
            saveService.SaveProgress();
        }
        
        public class Factory : PlaceholderFactory<AutoSaveData>
        {
        }
    }
}