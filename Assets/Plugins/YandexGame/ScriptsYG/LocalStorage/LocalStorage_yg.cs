using UnityEngine;
using System.Runtime.InteropServices;
using System;
using Cysharp.Threading.Tasks;

namespace YG
{
    public partial class YandexGame
    {
#region Internal methods
        [DllImport("__Internal")]
        private static extern void SaveToLocalStorage(string key, string value);
        [DllImport("__Internal")]
        private static extern string LoadFromLocalStorage(string key);
        [DllImport("__Internal")]
        private static extern int HasKeyInLocalStorage(string key);
        [DllImport("__Internal")]
        private static extern void RemoveFromLocalStorage(string key);
#endregion Internal methods
#region Player Data
 #region Save progress
        private static void SaveLocalPlayerData(string saveData)
        {
            Message("Save Local player data");
            SaveToLocalStorage(PLAYER_DATA_PATH, saveData);
        }
#endregion Save progress
 #region Load progress
        private static string LoadLocalPlayerData()
        {
            string storage = LoadFromLocalStorage(PLAYER_DATA_PATH);
            Message($"Load local player data: {storage}");
            return storage;
        } 
#endregion Load progress
#endregion Player Data
#region Helper methods
        private static bool HasKey(string key)
        {
            try
            {
                return HasKeyInLocalStorage(key) == 1;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                return false;
            }
        }
#endregion Helper methods
#region Remove progress
        public void RemoveLocalSaves() => RemoveFromLocalStorage(PLAYER_DATA_PATH);
#endregion Remove progress
    }
}
