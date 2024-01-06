using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CodeBase.Editor
{
    public class BuildNumberUpdate : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;
        private int buildNumber;

        public void OnPostprocessBuild(BuildReport report)
        {
            string version = Application.version;
            string[] parts = version.Split('.');
            if (int.TryParse(parts[2], out int lastPartAsInt))
            {
                lastPartAsInt += 1;
                buildNumber = lastPartAsInt;
            }
            else
            {
                Debug.LogError("Unable to parse version number");
            }

            PlayerSettings.bundleVersion = $"0.0.{buildNumber}";
        }
    }
}