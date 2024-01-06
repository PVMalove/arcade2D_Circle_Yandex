using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Core.Services.PauseService
{
    public class PauseService : IPauseService
    {
        private readonly List<IPauseHandler> handlers = new();
       
        public void Register(IPauseHandler pauseHandler) => 
            handlers.Add(pauseHandler);

        public void Unregister(IPauseHandler pauseHandler) => 
            handlers.Remove(pauseHandler);

        public void SetPause(bool isPaused)
        {
            Time.timeScale = isPaused ? 0 : 1;
            foreach (IPauseHandler handler in handlers) 
                handler.OnPauseChanged(isPaused);
        }
    }
}