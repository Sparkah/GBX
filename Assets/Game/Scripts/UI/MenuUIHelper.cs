using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.Events;
using Game.Audio.Scripts;

namespace Assets.Game.Scripts.UI
{
    public class MenuUIHelper : MonoBehaviour
    {
        private bool IsPaused => Time.timeScale < 1;

        [SerializeField]
        private UnityEvent OnPause;

        [SerializeField]
        private UnityEvent OnResume;

        private void OnEnable()
        {
            SetPause(true);
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            PauseInputHandler();
        }

        #region Pause

        private void PauseInputHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SetPause(!IsPaused);
        }

        public void Play()
        {
            SetPause(false);
        }

        public void PauseToggle()
        {
            SetPause(!IsPaused);
        }

        public void SetPause(bool value)
        {
            Time.timeScale = value ? 0 : 1;

            if (value)
            {
                OnPause?.Invoke();
                AudioSystem.StopSceneMusicAction?.Invoke(false);
                AudioPlayer.StatBackgroundMusicAction?.Invoke(false);
            }
            else
            {
                OnResume?.Invoke();
                AudioSystem.StopSceneMusicAction?.Invoke(true);
                AudioPlayer.StatBackgroundMusicAction?.Invoke(true);
            }
        }

        #endregion Pause

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}