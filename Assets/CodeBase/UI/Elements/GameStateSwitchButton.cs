using CodeBase.Core.GameFlow.GameHUD.State;
using CodeBase.Core.Infrastructure.States;
using CodeBase.Core.Services.LogService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
    public enum TargetStates
    {
        None = 0,
        Loading = 1,
        GameHub = 2,
        Gameplay = 3,
    }
    
    public class GameStateSwitchButton : MonoBehaviour
    {
        [SerializeField] private TargetStates targetState = 0;
        [SerializeField] private Button button;

        private SceneStateMachine sceneStateMachine;
        private ILogService log;

        [Inject]
        void Construct(SceneStateMachine sceneStateMachine,
            ILogService log)
        {
            this.sceneStateMachine = sceneStateMachine;
            this.log = log;
        }

        private void OnEnable() => 
            button.onClick.AddListener(OnClick);

        private void OnDisable() => 
            button.onClick.RemoveListener(OnClick);

        private async void OnClick()
        {
            await sceneStateMachine.Enter<FinishHUDSceneState, TargetStates>(targetState);
        }
    }
}