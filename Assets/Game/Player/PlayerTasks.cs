using Game.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerTasks : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        
        [Inject] private TasksSystem _tasksSystem;
        
        private void Awake()
        {
            _playerController.OnPlayerMoved += CompleteMoveTask;
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            var layer = LayerMask.NameToLayer("Chill");
            if (col.gameObject.layer == layer)
            {
                _tasksSystem.EngageChillTask();
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