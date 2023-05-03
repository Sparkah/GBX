using System;
using Game.Player;
using UnityEngine;
using System.Collections;

namespace Assets.Game.Scripts
{
    /// <summary>
    /// Как работает: Если игрок касается коллайдера, то после ожидания, ожидается ввод, если игрок больше не касается коллайдера, то ввод не ожидается. Если игрок взаимодействует с платформой, то коллайдер отключается, и через время включается обратно.
    /// </summary>
    [SelectionBase]
    [RequireComponent(typeof(PlatformEffector2D), typeof(Collider2D))]
    public class Platform : MonoBehaviour
    {
        [Range(0.1f, 1)]
        [SerializeField]
        [Tooltip("Время, которое игрок должен провести на платформе, пред тем как с нею взаимодействовать")]
        private float delayToInteraction = 0.2f;

        [Range(0.1f, 1)]
        [SerializeField]
        [Tooltip("Время отключение коллайдера")]
        private float maxTimeToPassThrough = 0.3f;

        private bool isReadyToInteract = true;
        private Collider2D[] _cachedColliders;

        private Coroutine timerToPassCoroutine;
        private Coroutine interactionWaiterCoroutine;

        private WaitForSeconds timerToIgnore;
        private WaitForSeconds timerToPassThrough;

#if UNITY_EDITOR

        private void OnValidate()
        {
            InitTimers();
        }

#endif

        #region Init

        private void Reset()
        {
            InitMonoBehavior();

            Collider2D[] arrayOfCurrentColliders = Array.FindAll(gameObject.GetComponents<Collider2D>(), elem => elem is not CompositeCollider2D);
            foreach (Collider2D collider in arrayOfCurrentColliders)
            {
                collider.usedByEffector = true;
            }

            GetComponent<PlatformEffector2D>().surfaceArc = 160;
        }

        private void InitMonoBehavior()
        {
            CacheAllColliders();

            foreach (Collider2D collider in _cachedColliders)
                collider.isTrigger = false;
        }

        private void CacheAllColliders()
        {
            _cachedColliders = Array.FindAll(gameObject.GetComponentsInChildren<Collider2D>(), elem => elem is not CompositeCollider2D);
        }

        private void Awake()
        {
            InitMonoBehavior();
            isReadyToInteract = true;
        }

        private void InitTimers()
        {
            timerToPassThrough = new WaitForSeconds(maxTimeToPassThrough);
            timerToIgnore = new WaitForSeconds(delayToInteraction);
        }

        #endregion Init

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController _))
            {
                StartWait();
            }
        }

        private void StartWait()
        {
            IgnoreInteractionForTime();
            interactionWaiterCoroutine = StartCoroutine(WaitPlayerInteraction());
        }

        private IEnumerator WaitPlayerInteraction()
        {
            yield return new WaitUntil(() => Input.GetAxis("Vertical") < 0 && isReadyToInteract);
            MakePlatformActive(false);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerController _))
                StopWait();
        }

        private void StopWait()
        {
            if (interactionWaiterCoroutine != null)
                StopCoroutine(interactionWaiterCoroutine);
        }

        private void MakePlatformActive(bool value)
        {
            if (!value)
                timerToPassCoroutine = StartCoroutine(TimerToEnable());
            else
                StopTimer();

            EnableCollider(value);
            IgnoreInteractionForTime();
        }

        private void EnableCollider(in bool value)
        {
            foreach (Collider2D collider in _cachedColliders)
                collider.enabled = value;
        }

        private void IgnoreInteractionForTime()
        {
            _ = StartCoroutine(TimerToDisableInteraction());
        }

        #region Timers

        private void StopTimer()
        {
            if (timerToPassCoroutine != null)
                StopCoroutine(timerToPassCoroutine);
        }

        private IEnumerator TimerToEnable()
        {
            yield return timerToPassThrough;
            MakePlatformActive(true);
        }

        private IEnumerator TimerToDisableInteraction()
        {
            isReadyToInteract = false;
            yield return timerToIgnore;
            isReadyToInteract = true;
        }

        #endregion Timers
    }
}