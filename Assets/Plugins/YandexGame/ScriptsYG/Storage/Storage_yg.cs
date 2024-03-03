using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
#if YG_NEWTONSOFT_FOR_SAVES
using Newtonsoft.Json;
#endif

namespace YG
{
    public partial class YandexGame
    {
#region Data
        private const string PLAYER_DATA_PATH = "savesDataProgress";
        private static readonly string PATH_SAVES_EDITOR = "//Plugins/YandexGame/WorkingData/Editor/SavesEditorYG.json";
        
        public static SavesYG savesData = new SavesYG();

        private static Action onResetProgress;
        private static UniTaskCompletionSource<string> CompletionSource { get; set; }
        private enum DataState { Exist, NotExist, Broken };

#region Internal methods
        [DllImport("__Internal")]
        private static extern string InitCloudStorage_js();
        [DllImport("__Internal")]
        private static extern void SaveYG(string jsonTechnicalData, string jsonPlayerData, bool flush); 
        [DllImport("__Internal")]
        private static extern void SaveYGTechnicalData(string jsonTechnicalData, bool flush);
        [DllImport("__Internal")]
        private static extern void LoadYG(bool sendback);
        [DllImport("__Internal")]
        private static extern void LoadYGPlayerData(bool sendback);
        
#endregion Internal methods         

        [InitYG]
        public static void InitCloudStorage()
        {
#if !UNITY_EDITOR
            Message("Init Storage inGame");
            Instance.SetLoadSaves(InitCloudStorage_js());
#else
            LoadProgress();
#endif
        }

#region Technical Data
 #region Save Load Editor
#if UNITY_EDITOR
            private static void SaveEditor()
            {
                Message("Save Editor");
                string path = Application.dataPath + PATH_SAVES_EDITOR;
                string directory = Path.GetDirectoryName(path);

                if (!Directory.Exists(directory))
                    if (directory != null)
                        Directory.CreateDirectory(directory);

                bool fileExits = File.Exists(path);

#if YG_NEWTONSOFT_FOR_SAVES
                string json = JsonConvert.SerializeObject(savesData, Formatting.Indented);
#else
                string json = JsonUtility.ToJson(savesData, true);
#endif
                File.WriteAllText(path, json);

                if (!fileExits && File.Exists(path))
                {
                  UnityEditor.AssetDatabase.Refresh();
                  Debug.Log("UnityEditor.AssetDatabase.Refresh");
                }
            }

            private static void LoadEditor()
            {
                Message("Load Editor");

                string path = Application.dataPath + PATH_SAVES_EDITOR;

                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
#if YG_NEWTONSOFT_FOR_SAVES
                    savesData = JsonConvert.DeserializeObject<SavesYG>(json);
#else
                    savesData = JsonUtility.FromJson<SavesYG>(json);
#endif
                    AfterLoading();
                }
                else
                {
                    ResetSaveProgress();
                }
            }
#endif  
#endregion Save Load Editor
 #region Save technical data
        public static void SaveProgress() => Instance._SaveProgress();

        private void _SaveProgress()
        {
            savesData.idSave++;
#if !UNITY_EDITOR
            if (!infoYG.saveCloud || (infoYG.saveCloud && infoYG.localSaveSync))
            {
                Message($"Start save technical data to json local: {JsonUtility.ToJson(savesData)}");
                SaveLocal();
            }

            if (infoYG.saveCloud && timerSaveCloud >= infoYG.saveCloudInterval + 1)
            {
                Message($"Start save technical data to json cloud: {JsonUtility.ToJson(savesData)}");
                timerSaveCloud = 0;
                SaveCloudTechnicalData();
            }
#else
            SaveEditor();
#endif
        }

        #endregion Save technical data

public static void SaveLocal()
{
    Message("Save Local technical data");
#if !UNITY_EDITOR
#if YG_NEWTONSOFT_FOR_SAVES
            SaveToLocalStorage("savesData", JsonConvert.SerializeObject(savesData));
#else
            SaveToLocalStorage("savesData", JsonUtility.ToJson(savesData));
#endif
#endif
}

#region Load technical data
        public static void LoadProgress() => Instance._LoadProgress();

        private void _LoadProgress()
        {
#if !UNITY_EDITOR
            if (!infoYG.saveCloud)
            {
                Message("Start load local technical saves");
                LoadLocal();
            }
            else 
            {
                Message("Start load cloud technical saves");
                LoadCloud();
            }   
#else
            LoadEditor();
#endif
        }

        private static void LoadLocal()
        {
            Message("Load Local technical data");

            if (!HasKey("savesData"))
                ResetSaveProgress();
            else
            {
#if YG_NEWTONSOFT_FOR_SAVES
                savesData = JsonConvert.DeserializeObject<SavesYG>(LoadFromLocalStorage("savesData"));
#else
                savesData = JsonUtility.FromJson<SavesYG>(LoadFromLocalStorage("savesData"));
#endif
            }
            AfterLoading();
        }
#endregion Load technical data
 #region Reset technical data
        public static void ResetSaveProgress() => Instance._ResetSaveProgress();

        private void _ResetSaveProgress()
        {
            Message("Reset Save Progress");
            savesData = new SavesYG { isFirstSession = false };
            onResetProgress?.Invoke();
            GetDataInvoke();
        }
#endregion Reset technical data
#endregion Technical Data
#region Player Data
 #region Save progress
        public static void SaveProgressPlayerData(string json) =>
            Instance._SaveProgressPlayerData(json);

        private void _SaveProgressPlayerData(string saveData)
        {
                savesData.idSave++;
#if !UNITY_EDITOR
            if (!infoYG.saveCloud || (infoYG.saveCloud && infoYG.localSaveSync))
            {
                Message($"Start save player progress to json local - Data: {saveData}");
                SaveLocalPlayerData(saveData);
                SaveLocal();
            }

            if (infoYG.saveCloud && timerSaveCloud >= infoYG.saveCloudInterval + 1)
            {
                timerSaveCloud = 0;
                Message($"Start save player progress to json cloud - Data: {saveData}");
                SaveCloud(saveData);
            }
#endif
        }
#endregion Save progress
 #region Load progress

        public static async UniTask<string> LoadProgressPlayerDataAsync(CancellationToken token) =>
            await Instance._LoadProgressPlayerDataAsync(token);

        private async UniTask<string> _LoadProgressPlayerDataAsync(CancellationToken token)
        {
            if (!infoYG.saveCloud)
            {
                Message("Start load local player saves");
                return LoadLocalPlayerData();
            }
            else
            { 
                Message("Start load cloud player saves");
                return await LoadCloudPlayerDataAsync(token);
            }
        }
        
#endregion Load progress
#endregion Player Data
#region Save end Load Cloud

        private static void SaveCloud(string savedPlayerData = "")
        {
#if YG_NEWTONSOFT_FOR_SAVES
            SaveYG(JsonConvert.SerializeObject(savesData), savedPlayerData, Instance.infoYG.flush);
#else
            SaveYG(JsonUtility.ToJson(savesData), savedPlayerData, Instance.infoYG.flush);
#endif
        }
        
        private static void SaveCloudTechnicalData()
        {
#if YG_NEWTONSOFT_FOR_SAVES
            SaveYGTechnicalData(JsonConvert.SerializeObject(savesData), Instance.infoYG.flush);
#else
            SaveYGTechnicalData(JsonUtility.ToJson(savesData), Instance.infoYG.flush);
#endif
        }
        
        public static void LoadCloud()
        {
#if !UNITY_EDITOR
            LoadYG(true);
#else
            LoadEditor();
            GetDataEvent?.Invoke();
#endif
        }

        private static async UniTask<string>LoadCloudPlayerDataAsync(CancellationToken token)
        {
            CompletionSource = new UniTaskCompletionSource<string>();
            token.Register(() => CompletionSource.TrySetCanceled());
            LoadYGPlayerData(true);
            return await CompletionSource.Task;
        }

#endregion Save end Load Cloud
#region Loading progress
        
        private SavesYG cloudData = new SavesYG();
        private SavesYG localData = new SavesYG();

        public void SetLoadSaves(string data)
        {
            DataState cloudDataState = DataState.Exist;
            DataState localDataState = DataState.Exist;

            if (data != "noData")
            {
                string parsingData = ParsingData(data);
                try
                {
#if YG_NEWTONSOFT_FOR_SAVES
                    cloudData = JsonConvert.DeserializeObject<SavesYG>(parsingData);
#else
                    cloudData = JsonUtility.FromJson<SavesYG>(parsingData);
#endif
                }
                catch (Exception e)
                {
                    Debug.LogError("Cloud load technical data Error: " + e.Message);
                    cloudDataState = DataState.Broken;
                }
            }
            else cloudDataState = DataState.NotExist;
            
            if (infoYG.localSaveSync == false)
            {
                if (cloudDataState == DataState.NotExist)
                {
                    Message("No cloud saves technical data. Local saves are disabled.");
                    ResetSaveProgress();
                }
                else
                {
                    Message(cloudDataState == DataState.Broken
                        ? "Load Cloud technical data Broken! But we tried to restore and load cloud saves. Local saves are disabled."
                        : "Load Cloud technical data Complete! Local saves are disabled.");

                    savesData = cloudData;
                    AfterLoading();
                }
                return;
            }

            if (HasKey("savesData"))
            {
                try
                {
#if YG_NEWTONSOFT_FOR_SAVES
                    localData = JsonConvert.DeserializeObject<SavesYG>(LoadFromLocalStorage("savesData"));
#else
                    localData = JsonUtility.FromJson<SavesYG>(LoadFromLocalStorage("savesData"));
#endif
                }
                catch (Exception e)
                {
                    Debug.LogError("Local load technical data Error: " + e.Message);
                    localDataState = DataState.Broken;
                }
            }
            else localDataState = DataState.NotExist;
            
            Message("Technical data localDataState - " + localDataState);
            Message("Technical data cloudDataState - " + cloudDataState);
            
            switch (cloudDataState)
            {
                case DataState.Exist when localDataState == DataState.Exist:
                {
                    if (cloudData.idSave >= localData.idSave)
                    {
                        Message($"Load cloud technical data Complete! ID Cloud Save: {cloudData.idSave}, ID Local Save: {localData.idSave}");
                        savesData = cloudData;
                    }
                    else
                    {
                        Message($"Load local technical data Complete! ID Cloud Save: {cloudData.idSave}, ID Local Save: {localData.idSave}");
                        savesData = localData;
                    }
                    AfterLoading();
                    break;
                }
                case DataState.Exist:
                    savesData = cloudData;
                    Message("Load cloud technical data Complete! Local Data - " + localDataState);
                    AfterLoading();
                    break;
                default:
                {
                    if (localDataState == DataState.Exist)
                    {
                        savesData = localData;
                        Message("Load local technical data Complete! Cloud Data - " + cloudDataState);
                        AfterLoading();
                    }
                    else if (cloudDataState == DataState.Broken ||
                             (cloudDataState == DataState.Broken && localDataState == DataState.Broken))
                    {
                        Message("Local saves technical data- " + localDataState);
                        Message("Cloud saves technical data - Broken! Data Recovering...");
                        ResetSaveProgress();
#if YG_NEWTONSOFT_FOR_SAVES
                        savesData = JsonConvert.DeserializeObject<SavesYG>(data);
#else
                        savesData = JsonUtility.FromJson<SavesYG>(data);
#endif
                        Message("Cloud saves technical data Partially Restored!");
                        AfterLoading();
                    }
                    else if (localDataState == DataState.Broken)
                    {
                        Message("Cloud saves technical data - " + cloudDataState);
                        Message("Local saves technical data - Broken! Data Recovering...");
                        ResetSaveProgress();
#if YG_NEWTONSOFT_FOR_SAVES
                        savesData = JsonConvert.DeserializeObject<SavesYG>(LoadFromLocalStorage("savesData"));
#else
                        savesData = JsonUtility.FromJson<SavesYG>(LoadFromLocalStorage("savesData"));
#endif
                        Message("Local saves technical data Partially Restored!");
                        AfterLoading();
                    }
                    else
                    {
                        Message("No saves technical data");
                        ResetSaveProgress();
                    }
                    break;
                }
            }
        }

        [UsedImplicitly]
        public void SetLoadSavesPlayerDataAsync(string data)
        {
            DataState cloudPlayerDataState = DataState.Exist;
            DataState localPlayerDataState = DataState.Exist;
            
            string cloudPlayerData = null;
            string localPlayerData = null;
            
            if (data != "noData")
            {
                string parsingData = ParsingData(data);
                try
                {
                    Message($"Load player data from the server after a request to the Yandex cloud");
                    cloudPlayerData = parsingData;
                }
                catch (Exception e)
                {
                    Debug.LogError("Cloud load player data error: " + e.Message);
                    cloudPlayerDataState = DataState.Broken;
                }
            }
            else cloudPlayerDataState = DataState.NotExist;

            if (infoYG.localSaveSync == false)
            {
                if (cloudPlayerDataState == DataState.NotExist)
                {
                    Message("No cloud saves player data. Local saves are disabled.");
                    ResetSaveProgress();
                }
                else
                {
                    Message(cloudPlayerDataState == DataState.Broken
                        ? "Load Cloud player data Broken! But we tried to restore and load cloud saves. Local saves are disabled."
                        : "Load Cloud player data Complete! Local saves are disabled.");

                    CompletionSource.TrySetResult(String.Empty);
                }
                return;
            }

            if (HasKey(PLAYER_DATA_PATH))
            {
                try
                {
                    localPlayerData = LoadFromLocalStorage(PLAYER_DATA_PATH);
                }
                catch (Exception e)
                {
                    Debug.LogError("Local load player data Error: " + e.Message);
                    localPlayerDataState = DataState.Broken;
                }
            }
            else localPlayerDataState = DataState.NotExist;
            
            Message("Player data localDataState - " + localPlayerDataState);
            Message("Player data cloudDataState - " + cloudPlayerDataState);
            
            switch (cloudPlayerDataState)
            {
                case DataState.Exist when localPlayerDataState == DataState.Exist:
                {
                    if (cloudData.idSave >= localData.idSave)
                    {
                        Message($"Load player data cloud Complete! ID Cloud Save: {cloudData.idSave}, ID Local Save: {localData.idSave}");
                        CompletionSource.TrySetResult(cloudPlayerData);
                    }
                    else
                    {
                        Message($"Load player data local Complete! ID Cloud Save: {cloudData.idSave}, ID Local Save: {localData.idSave}");
                        CompletionSource.TrySetResult(localPlayerData);
                    }
                    break;
                }
                case DataState.Exist:
                    CompletionSource.TrySetResult(cloudPlayerData);
                    Message("Load cloud player data Complete! Cloud Data  - " + cloudPlayerDataState);
                    break;
                default:
                {
                    if (localPlayerDataState == DataState.Exist)
                    {
                        CompletionSource.TrySetResult(localPlayerData);
                        Message($"Load local player data Complete Data: {localPlayerData} ! Local Data - " + localPlayerDataState);
                    }
                    else if (cloudPlayerDataState == DataState.Broken ||
                             (cloudPlayerDataState == DataState.Broken && localPlayerDataState == DataState.Broken))
                    {
                        Message("Local Saves - " + localPlayerDataState);
                        Message("Cloud Saves - Broken! Data Recovering...");

                        string parsingData = ParsingData(data);
                        CompletionSource.TrySetResult(parsingData);
                        Message("Cloud Saves Partially Restored!");
                    }
                    else if (localPlayerDataState == DataState.Broken)
                    {
                        Message("Cloud Saves - " + cloudPlayerDataState);
                        Message("Local Saves - Broken! Data Recovering...");

                        string storage = LoadFromLocalStorage(PLAYER_DATA_PATH);
                        CompletionSource.TrySetResult(storage);
                        Message("Local Saves Partially Restored!");
                    }
                    else
                    {
                        Message("Player data No Saves");
                        CompletionSource.TrySetResult(String.Empty);
                    }
                    break;
                }
            }
        }
        
#endregion Loading progress
#region Helper methods
        private static void AfterLoading()
        {
            Message($"After loading Data");
            GetDataInvoke();
            Message($"After loading Data complete: SDKEnabled:{_SDKEnabled}, GetDataEvent: {GetDataEvent}");
        }
        
        private string ParsingData(string data)
        {
            data = data.Trim('[', ']', '"', ' ');
            data = data.Replace(@"\n", ""); //todo delete
            data = data.Replace(@"\", ""); //todo delete
            Message($"ParsingData data server: {data}");
            return data;
        }
#endregion Helper methods
#endregion Data
    }
}
