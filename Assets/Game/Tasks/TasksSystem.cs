using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Tasks
{
    public class TasksSystem : MonoBehaviour
    {
        [ShowInInspector] private int _currentTask;
        [ShowInInspector] private int _currentTaskList;
        private List<TaskSO> _activeTasks = new List<TaskSO>();
        public List<TaskSO> ActiveTasks => _activeTasks;
        
        [SerializeField] private TaskListSO[] _listOfTasks;

        [Inject] private World _world;

        public event Action<int> OnTaskCompleted;

        public event Action<List<TaskSO>> OnTasksSetUp;

        private void Awake()
        {
            _currentTaskList = _world.CurrentTaskListID.Value;
            _currentTask = _world.CurrentTaskID.Value;
            SetUpTasksList();
        }

        private void SetUpTasksList()
        {
            SetActiveTasksFromCurrentTasksList();
        }

        private void SetActiveTasksFromCurrentTasksList()
        {
            _activeTasks.Clear();
            var currentList = GetAllTasksInList();
            if (currentList != null)
                foreach (var task in currentList)
                {
                    _activeTasks.Add(task);
                    IterateAndResetTasks(task);
                }

            OnTasksSetUp?.Invoke(_activeTasks);
        }

        private void IterateAndResetTasks(TaskSO task)
        {
            if (task.TaskType == TaskType.Chill)
            {
                _chillTaskCompleted = false;
            }
        }

        private void CompleteTask(int taskID)
        {
            OnTaskCompleted?.Invoke(taskID);
            _currentTask += 1;
            CheckLeftoverTasks();
        }

        private void CheckLeftoverTasks()
        {
            if (_currentTask < GetAllTasksInList()!.Length) return;
            _currentTaskList += 1;
            _currentTask = 0;
            SetActiveTasksFromCurrentTasksList();
        }
        

        private TaskSO GetCurrentTask()
        {
            return _listOfTasks[_currentTaskList].Tasks[_currentTask];
        }

        [CanBeNull]
        private TaskSO[] GetAllTasksInList()
        {
            try
            {
                return _listOfTasks[_currentTaskList].Tasks;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("Win game");
                return null;
            }
        }

        private void OnDestroy()
        {
            _world.CurrentTaskID.Value = _currentTask;
        }


        #region Chill Task

        private bool _chillTaskCompleted;
        private float _timer;
        public void EngageChillTask()
        {
            if (_chillTaskCompleted) return;
            foreach (var task in _activeTasks)
            {
                if (task.TaskType != TaskType.Chill)
                {
                    return;
                }
                
                _timer += Time.deltaTime;

                if (_timer >= ReturnChillTaskTimer())
                {
                    _chillTaskCompleted = true;
                    CompleteChillTask();
                    return;
                }
            }
        }

        private float ReturnChillTaskTimer()
        {
            foreach (var task in _activeTasks.Where(task => task.TaskType == TaskType.Chill))
            {
                return task.Time;
            }
            
            return 5;
        }
        
        private void CompleteChillTask()
        {
            _timer = 0;
            var id = 0;
            foreach (var task in _activeTasks)
            {
                if (task.TaskType == TaskType.Chill)
                {
                    CompleteTask(id);
                    return;
                }
                id += 1;
            }
        }

        #endregion

        #region Move Task

        public void CompleteMoveTask()
        {
            var id = 0;
            foreach (var task in _activeTasks)
            {
                if (task.TaskType == TaskType.Move)
                {
                    CompleteTask(id);
                    return;
                }
                id += 1;
            }
        }

        #endregion
        
        #region Touch Task

        public void CompletePassObjectTask()
        {
            var id = 0;
            foreach (var task in _activeTasks)
            {
                if (task.TaskType == TaskType.PassObject)
                {
                    CompleteTask(id);
                    return;
                }
                id += 1;
            }
        }

        #endregion
        
        #region Find Object Task

        

        #endregion
        
        #region Jump On Task

        

        #endregion
        
        #region Pass Object Task

        

        #endregion
        
        #region Break Object Task

        

        #endregion
        
        #region Collect All Objects Task

        

        #endregion
    }
}