namespace CodeBase.Core.Infrastructure
{
    public class InfrastructureAssetPath
    {
        public const string HUDRoot = "Infrastructure/HUD Root";

        //Infrastructure
        public const string GameBootstraperPath = "Infrastructure/Bootstraper/GameBootstraper";
        public const string AudioServicePath = "Infrastructure/AudioService/AudioService";
        public const string AutoSaveServicePath = "Infrastructure/AutoSaveData";
        public const string PlayerFacadePath = "Infrastructure/GamePlayerContext";

        //Scene
        public const string GameHubScene = "GameHub";
        public const string GameModeScene = "GameMode";
        public const string GameLoadingScene = "LoadingScene";
        
        //Gameplay
        public const string SlingPath = "Gameplay/Level/Sling";

        //UI
        public const string UIRoot = "Infrastructure/UI/GameUICanvas";
        public const string CurtainPath = "CurtainCanvas";
        public const string AwaitingOverlayPath = "AwaitingOverlay";
        public const string BuildInfoPath = "UI/HUD/BuildInfo";
        public const string SettingBar = "UI/HUD/Setting/SettingBar";
    }
}