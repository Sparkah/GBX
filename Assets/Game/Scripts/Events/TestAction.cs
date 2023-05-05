using System;
using Game.Player;

using UnityEngine;
using UnityEngine.Events;

namespace Assets.Game.Scripts.Events
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class TestAction : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshPro interactionText;

        [SerializeField] private PlayerTasks _playerTasks;

        public event Action OnInteraction;

        [SerializeField]
        private bool isSingleUse = true;

        [SerializeField, HideInInspector]
        private Transform _interactionViewer;

        [SerializeField, HideInInspector]
        private BoxCollider2D _boxCollider;
        
        

#if UNITY_EDITOR

        private void Reset()
        {
            _boxCollider = GetComponent<BoxCollider2D>();
            _boxCollider.isTrigger = true;
        }

        private void OnValidate()
        {
            _boxCollider = _boxCollider != null ? GetComponent<BoxCollider2D>() : _boxCollider;

            if (_boxCollider != null)
                _boxCollider.isTrigger = true;

            if (interactionText != null)
                _interactionViewer = interactionText.transform;
        }

#endif

        private Transform tempPlayer;
        private Transform playerCamera;
        private bool _isPlayerInteracting = false;
        private bool _isUsed = false;

        private void Awake()
        {
            playerCamera = FindObjectOfType<Camera>()?.transform;//��� ���� ����� �� ����� ������ ����� ������ ���������

            if (interactionText == null)
                return;

            _interactionViewer.gameObject.SetActive(false);
            _interactionViewer.eulerAngles = Vector3.up * 90f;
        }

        private void OnEnable()
        {
            _playerTasks._testAction = this;
        }

        private void Update()
        {
            if (!_isPlayerInteracting)
                return;

            if (Input.GetKeyDown(KeyCode.E))
            {
                _isUsed = true;

                if (isSingleUse)
                    RemoveAction();
                
                OnInteraction?.Invoke();
            }

            if (interactionText == null)
                return;
            _interactionViewer.position = GetPositionToPlaceViewer();
            _interactionViewer.LookAt(playerCamera, -Vector3.up);
        }

        private void RemoveAction()
        {
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController player))
            {
                tempPlayer = player.transform;
                _interactionViewer.gameObject.SetActive(true);
                _isPlayerInteracting = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerController _))
            {
                _interactionViewer.gameObject.SetActive(false);
                _isPlayerInteracting = false;
            }
        }

        private Vector3 GetPositionToPlaceViewer()
        {
            return tempPlayer == null ? Vector3.zero : _boxCollider.bounds.ClosestPoint(tempPlayer.position) + Vector3.up;
        }
    }
}