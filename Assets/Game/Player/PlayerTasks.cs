using Game.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerTasks : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        [Inject] private TasksSystem _tasksSystem;
        private int _chillLayer, _touchLayer;
        
        private void Awake()
        {
            _playerController.OnPlayerMoved += CompleteMoveTask;
            _chillLayer = LayerMask.NameToLayer("Chill");
            _touchLayer = LayerMask.NameToLayer("Touch");
        }
        
        private void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.layer == _chillLayer)
            {
                _tasksSystem.EngageChillTask();
            }
        }

        private bool _hasTouched;
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.layer == _touchLayer)
            {
                if (!_hasTouched)
                {
                    _hasTouched = true;
                    col.GetComponent<Rigidbody2D>().gravityScale = 1;
                    _tasksSystem.CompletePassObjectTask();
                }
            }
        }

        private bool _hasMoved;
        private void CompleteMoveTask()
        {
            if (!_hasMoved)
            {
                _hasMoved = true;
                _tasksSystem.CompleteMoveTask();
            }
        }

        private void OnDestroy()
        {
            _playerController.OnPlayerMoved -= CompleteMoveTask;
        }
    }
}