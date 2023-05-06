using System.Collections;
using Assets.Game.Scripts.Events;
using Game.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace Game.Player
{
    public class PlayerTasks : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;
        [HideInInspector] public TestAction _testAction;
        
        [Inject] private TasksSystem _tasksSystem;
        private int _chillLayer, _touchLayer;

        public void ResetUp(TestAction testAction)
        {
            _testAction = testAction;
            _testAction.OnInteraction+=CompleteInterractionTask;
        }
        
        private void Awake()
        {
            _playerController.OnPlayerMoved += CompleteMoveTask;
            _chillLayer = LayerMask.NameToLayer("Chill");
            _touchLayer = LayerMask.NameToLayer("Touch");
        }

        private void CompleteInterractionTask()
        {
            _tasksSystem.CompleteInterractTask();
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
                    _tasksSystem.CompletePassObjectTask(col.gameObject.GetComponent<TaskTouchReference>()._taskSo.Text);
                    StartCoroutine(EnableTouch());
                }
            }
        }

        private IEnumerator EnableTouch()
        {
            yield return new WaitForSeconds(1f);
            _hasTouched = false;
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