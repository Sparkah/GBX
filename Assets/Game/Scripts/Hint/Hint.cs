using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.ParticleSystem;

namespace Assets.Game.Scripts.Hint
{
    [RequireComponent(typeof(ParticleSystem))]
    public class Hint : MonoBehaviour
    {
        [SerializeField]
        [Range(1, 60)]
        private float timeToShowHint = 20;

        [SerializeField, HideInInspector]
        private ParticleSystem _particle;

        private WaitForSeconds _hintTimer;
        private Coroutine _hintTimerCoroutine;

        #region Init

#if UNITY_EDITOR

        private void OnValidate()
        {
            Init();
        }

#endif

        private void Reset()
        {
            Init();
        }

        private void Init()
        {
            _particle = _particle == null ? GetComponent<ParticleSystem>() : _particle;
            _hintTimer = new WaitForSeconds(timeToShowHint);
        }

        private void Awake()
        {
            Init();
        }

        #endregion Init

        private void OnEnable()
        {
            _particle.Stop();
            ShowHitTimer();
        }

        private void OnDisable()
        {
            StopTimer();
        }

        private void ShowHitTimer()
        {
            _hintTimerCoroutine = StartCoroutine(TimerToShowHint());
        }

        private IEnumerator TimerToShowHint()
        {
            yield return _hintTimer;
            PlayParticle();
        }

        [ContextMenu("Play Particle")]
        public void PlayParticle()
        {
            _particle.Play();
        }

        [ContextMenu("Stop timer")]
        public void StopTimer()
        {
            if (_hintTimerCoroutine != null)
            {
                StopCoroutine(_hintTimerCoroutine);
            }
        }
    }
}