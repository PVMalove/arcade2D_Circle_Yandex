using System.Collections.Generic;
using CodeBase.Core.Services.ProgressService;
using CodeBase.Gameplay.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Core.Infrastructure.Factories
{
    public interface IGameFactory
    {
        List<IProgressReader> ProgressReaders { get; }
        List<IProgressSaver> ProgressWriters { get; }
        GameObject CircleBackground { get; }
        GameObject CreateHUD();
        GameObject CreateCircleBackground();
        UniTask<CircleHero> CreateCircleHero();
        void Cleanup();
    }
}