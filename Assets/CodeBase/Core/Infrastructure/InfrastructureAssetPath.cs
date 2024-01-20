namespace CodeBase.Core.Infrastructure
{
    public class InfrastructureAssetPath
    {
        public const string HUDRoot = "Infrastructure/HUD Root";

        //Infrastructure
        public const string GameBootstraperPath = "Infrastructure/Bootstraper/GameBootstraper";
        public const string AudioServicePath = "Infrastructure/AudioService/AudioService";
        public const string AutoSaveServicePath = "Infrastructure/AutoSaveData";

        //Scene
        public const string GameScene = "GameScene";
        public const string GameLoadingScene = "LoadingScene";
        
        //Gameplay

        //UI
        public const string UIRoot = "Infrastructure/UI/GameUICanvas";
        public const string CurtainPath = "CurtainCanvas";
        public const string AwaitingOverlayPath = "AwaitingOverlay";
        public const string BuildInfoPath = "UI/HUD/BuildInfo";
        public const string SettingBar = "UI/HUD/Setting/SettingBar";
        public const string CircleBackground = "Gameplay/Enviroment/CircleBackground";
    }
}