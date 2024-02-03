using CodeBase.StaticData.Level;
using CodeBase.StaticData.UI;
using Cysharp.Threading.Tasks;

namespace CodeBase.Core.Services.StaticDataService
{
    public interface IStaticDataService
    {
        UniTask InitializeAsync();
        CharacterConfig CharacterConfig { get; }
        WindowsConfig WindowsConfig { get; }
        PopupsConfig PopupsConfig { get; }
    }
}