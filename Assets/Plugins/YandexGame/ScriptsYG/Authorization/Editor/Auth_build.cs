using UnityEngine;
using System.IO;

namespace YG.EditorScr.BuildModify
{
    public partial class ModifyBuildManager
    {
        public static void AuthPlayer()
        {
            string initFunction = "playerData = await InitPlayer();\nconsole.log('[INDEX] -> Init player SDK');";
            AddIndexCode(initFunction, CodeType.init);

            string donorPatch = Application.dataPath + "/Plugins/YandexGame/ScriptsYG/Authorization/Editor/Auth_js.js";
            string donorText = File.ReadAllText(donorPatch);
            donorText = donorText.Replace("___scopes___", infoYG.scopes.ToString().ToLower());
            donorText = donorText.Replace("___photoSize___", infoYG.GetPlayerPhotoSize());

            AddIndexCode(donorText, CodeType.js);
        }
    }
}
