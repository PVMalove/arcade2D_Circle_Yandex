using UnityEngine;

namespace CodeBase.Core.Services.LogService
{
    public class LogService : ILogService
    {
        private const bool StateLog = true;
        private const bool ServiceLog = true;
        private const bool yandexLog = true;
        private const bool audioLog = true;
        
        public void Log(string msg) => 
            Debug.Log(msg);
        
        public void LogState(string msg, object obj)
        {
            string className = obj.GetType().Name;
            if (StateLog) Debug.Log($"[State] -> [{className}] -> {msg}");
        }

        void ILogService.LogService(string msg, object obj)
        {
            string className = obj.GetType().Name;
            if (ServiceLog) Debug.Log($"[Service] -> [{className}] -> {msg}");
        }

        public void LogYandex(string msg, object obj)
        {
            string className = obj.GetType().Name;
            if (yandexLog) Debug.Log($"[YandexGame] -> [{className}] -> {msg}");
        }

        public void LogAudio(string msg, object obj)
        {
            string className = obj.GetType().Name;
            if (audioLog) Debug.Log($"[Audio] --> [{className}] -> {msg}");
        }
        
        public void LogError(string msg) => 
            Debug.LogError(msg);

        public void LogWarning(string msg) => 
            Debug.LogWarning(msg);
    }
}