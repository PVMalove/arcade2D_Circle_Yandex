using System;
using CodeBase.Core.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace CodeBase.Audio.Core
{
    public class SourceAudio : MonoBehaviour
    {
        private const float MAX_MIXER_VOLUME = 0.0f;
        private const float MIN_MIXER_VOLUME = -80.0f;

        [SerializeField] private string musicGroup = "Music";
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private AudioSource unitySource;
        
        public event Action OnFinished;
        
        private IAudioManagement audioManagement;
        private ILogService log;
        private bool beginPlaying;
        private bool loadClip;
        private AudioClip clip;
        private float lastTime;
        private string CurrentKey;
        private bool isFocus = true;
        
        private void OnValidate()
        {
            if (TryGetComponent(out unitySource) == false)
            {
                unitySource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        [Inject]
        public void Construct(IAudioManagement audioManagement,ILogService log)
        {
            this.log = log;
            this.audioManagement = audioManagement;
        }
        
        private void OnEnable()
        {
            AppFocusHandle.OnFocus += OnAudioPaused;
            AppFocusHandle.OnUnfocus += OnAudioUnpaused;
        }

        private void OnDisable()
        {
            AppFocusHandle.OnFocus -= OnAudioPaused;
            AppFocusHandle.OnUnfocus -= OnAudioUnpaused;
        }

        private void Update()
        {
            if (isFocus == false) return;
            CheckFinished();
        }

        public void Play(string key)
        {
            unitySource.Stop();
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError("[SourceAudio] key is empty, Source Audio PlaySound: " + gameObject.name);
                return;
            }

            PlayRoutineAsync(key).Forget();
        }
        
        private async UniTaskVoid PlayRoutineAsync(string key)
        {
            CurrentKey = key;
            beginPlaying = false;
            loadClip = true;
            clip = null;

            clip = await audioManagement.GetClip(CurrentKey);
            log.LogAudio($"Play Routine key: {CurrentKey}. Get audio clip -> {clip.name} - {clip.length}", this);

            if (clip == null)
            {
                Debug.LogError("Audio Management not found clip at key: " + CurrentKey +
                               ",\n Source Audio PlaySound: " + gameObject.name);
                return;
            }

            unitySource.clip = clip;
            unitySource.Play();
            loadClip = false;
            beginPlaying = true; 
            lastTime = 0;
        }
        
        private void CheckFinished()
        {
            if (clip == null || loadClip)
                return;

            if (unitySource.time <= 0 && unitySource.isPlaying == false && beginPlaying)
            {
                beginPlaying = false;
                ClipFinished();
            }
        }

        private void ClipFinished()
        {
            log.LogAudio("Audio clip finished: " + CurrentKey, this);
            OnFinished?.Invoke();
        }
        
        private void OnAudioUnpaused()
        {
            if (isFocus == false && beginPlaying && lastTime > 0) 
                unitySource.time = lastTime;

            isFocus = true;
        }

        private void OnAudioPaused()
        {
            if (isFocus && beginPlaying) 
                lastTime = unitySource.time;

            isFocus = false;
        }
    }
}