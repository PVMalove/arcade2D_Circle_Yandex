using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Infrastructure.SceneManagement
{
    public interface ISceneLoader
    {
        UniTask Load(string nextScene);
    }
}