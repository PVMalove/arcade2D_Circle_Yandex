using UnityEditor;
using UnityEditor.SceneManagement;

namespace CodeBase.Editor
{
    [InitializeOnLoad]
    public class InitialSceneLoader
    {
        static InitialSceneLoader()
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }
    }
}