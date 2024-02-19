using UnityEngine;

namespace CodeBase.Core.Services.LogService
{
    public class LogService : ILogService
    {
        private const bool StateLog = false;
        private const bool ServiceLog = false;
        private const bool yandexLog = false;
        private const bool audioLog = false;
        
        public void Log(string msg) => 
            Debug.Log(msg);
        
        public void LogState(string msg, object obj)
        {
            if (StateLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[State] -> [{className}] -> {msg}");
            }
        }

        void ILogService.LogService(string msg, object obj)
        {
            if (ServiceLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[Service] -> [{className}] -> {msg}");
            }
        }

        public void LogYandex(string msg, object obj)
        {
            if (yandexLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[YandexGame] -> [{className}] -> {msg}");
            }
        }

        public void LogAudio(string msg, object obj)
        {
            if (audioLog)
            {
                string className = obj.GetType().Name;
                Debug.Log($"[Audio] --> [{className}] -> {msg}");
            }
        }
        
        public void LogError(string msg) => 
            Debug.LogError(msg);

        public void LogWarning(string msg) => 
            Debug.LogWarning(msg);
    }
}