namespace CodeBase.Core.Infrastructure
{
    public class InfrastructureAssetPath
    {
        public const string HUDRoot = "Infrastructure/HUD Root"; //TODO: delete

        //Infrastructure
        public const string GameBootstraperPath = "Infrastructure/Bootstraper/GameBootstraper";
        public const string NewSaveDataAddress = "FirstSaveData";
        public const string AudioServicePath = "Infrastructure/AudioService/AudioService";
        public const string AutoSaveServicePath = "Infrastructure/AutoSaveData";

        //Scene
        public const string GameSceneAddress = "GameScene";
        public const string GameLoadingSceneAddress = "LoadingScene";
        
        //Gameplay

        //UI
        public const string UIRoot = "Infrastructure/UI/GameUICanvas";
        public const string CurtainAddress = "CurtainCanvas";
        public const string AwaitingOverlayAddress = "AwaitingCanvas";
        public const string BuildInfoPath = "UI/HUD/BuildInfo";
        public const string SettingBar = "UI/HUD/Setting/SettingBar";
        public const string CircleBackground = "Gameplay/Enviroment/CircleBackground";
    }
}