using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Tasks.View
{
    public class TasksView : MonoBehaviour
    {
        [SerializeField] private List <TaskView> _tasks;
        
        [Inject] private TasksSystem _tasksSystem;

        private void Awake()
        {
            _tasksSystem.OnTasksSetUp += DisplayNewTasks;
            _tasksSystem.OnTaskCompleted += HideTask;
            _tasksSystem.OnTaskChillProgress += ShowTaskProgress;
        }

        private void DisplayNewTasks(List<TaskSO> tasks)
        {
            //ShowAllTasks(tasks);
            var taskAmount = 0;
            for (int i = 0; i < tasks.Count; i++)
            {
                _tasks[i].Text.text = tasks[i].Text;
                _tasks[i].Image.sprite = tasks[i].Sprite;
                taskAmount += 1;
            }
            
            HideTasks(taskAmount);
        }

        private void ShowTaskProgress(float current, float initial, int id)
        {
            Debug.Log(id);
            _tasks[id].Image.fillAmount = current / initial;
        }

        private void HideTasks(int amount)
        {
            for (int i = 0; i < _tasks.Count; i++)
            {
                if (i >= amount)
                {
                    _tasks[i].gameObject.SetActive(false);
                }
                else
                {
                    _tasks[i].gameObject.SetActive(true);
                }
            }
        }

        private void HideTask(int taskID)
        {
            _tasks[taskID].gameObject.SetActive(false);
            //_tasks.Remove(_tasks[taskID]);
        }
        
        private void OnDestroy()
        {
            _tasksSystem.OnTasksSetUp -= DisplayNewTasks;
            _tasksSystem.OnTaskCompleted -= HideTask;
            _tasksSystem.OnTaskChillProgress -= ShowTaskProgress;
        }
    }
}