using CodeBase.StaticData.Level;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        CharacterConfig CharacterConfig { get; }
    }
}