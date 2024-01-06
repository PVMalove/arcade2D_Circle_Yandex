using CodeBase.Core.Infrastructure.States.Infrastructure;

namespace CodeBase.Core.Infrastructure.States
{
    // Local state machine to switch scene states for example states of gameplay level.
    // Bind in scene contexts with different states according to scene logic.
    // Scene bootstrapers fill this FSM by states
    public class SceneStateMachine : StateMachine
    {
    }
}