using Game.Player;
using UnityEngine;
using Sirenix.Utilities;

namespace Assets.Game.Player
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField, HideInInspector]
        private PlayerController _playerController;

        [Header("Animator")]
        [SerializeField, HideInInspector]
        private Animator _animator;

        private Transform _playerTransform;
        private Vector3 _playerScale;

        [SerializeField]
        private string IdleName = "IDLE";

        [SerializeField]
        private string walkName = "Walk";

        [SerializeField]
        private string crouchName = "Crouch";

        [SerializeField]
        private string jumpName = "Jump";

        [SerializeField]
        private string inAirName = "inAir";

        #region Init

        private void Reset()
        {
            Init();
        }

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _playerController = _playerController == null ? GetComponent<PlayerController>() : _playerController;
            _animator = _animator == null ? GetComponent<Animator>() : _animator;

            _playerTransform = _playerController.transform;

            _playerScale = _playerTransform.localScale;
        }

        #endregion Init

        private void OnEnable()
        {
            _playerController.OnJumping += JumpCallback;
            //_playerController.OnCrouchingChanged += CrouchCallback;
        }

        private void OnDisable()
        {
            _playerController.OnJumping -= JumpCallback;
            //_playerController.OnCrouchingChanged -= CrouchCallback;
        }

        private void Update()
        {
            MovementHandler(_playerController.RawMovement);
            InAirHandler(!_playerController.Grounded);
        }

        private void MovementHandler(Vector2 direction)
        {
            bool isMoving = direction.x != 0;

            _animator.SetBool(IdleName, !isMoving);
            _animator.SetBool(walkName, isMoving);

            if (isMoving)
            {
                if (direction.x < 0)
                    _playerTransform.localScale = _playerScale;
                else
                    _playerTransform.localScale = new Vector3(-_playerScale.x, _playerScale.y, _playerScale.z);
            }
        }

        private void JumpCallback()
        {
            _animator.SetTrigger(jumpName);
        }

        private void InAirHandler(bool inAir)
        {
            _animator.SetBool(inAirName, inAir);
        }

        //private void CrouchCallback(bool value)
        //{
        //    _animator.SetBool(crouchName, value);
        //}
    }
}