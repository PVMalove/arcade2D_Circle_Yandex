using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public static class SaveLoadMenu
    {
        [MenuItem("Tools/MaloveGame/Save/Open Directory")]
        static void OpenDirectory()
        {
            Process.Start(Application.persistentDataPath);
        }

        [MenuItem("Tools/MaloveGame/Save/ClearSave")]
        static void ClearSave()
        {
            File.Delete(Application.persistentDataPath + "/Save.json");
        }
    }
}