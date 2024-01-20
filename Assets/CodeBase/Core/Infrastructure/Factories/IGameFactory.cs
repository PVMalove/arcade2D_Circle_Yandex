using System.Collections.Generic;
using CodeBase.Core.Services.PlayerProgressService;
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
        void Cleanup();
    }
}