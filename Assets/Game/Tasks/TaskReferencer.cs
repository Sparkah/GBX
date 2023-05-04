using System;
using UnityEngine;

namespace Game.Tasks
{
    public class TaskReferencer : MonoBehaviour
    {
        public TaskListSO TaskList;
        
        [SerializeField] private GameObject[] _objectsToActivate;
        [SerializeField] private GameObject[] _objectsToDeActivate;
        [SerializeField] private ParticleSystem[] _particleSystemsToPlay;

        public GameObject[] ObjectsToActivate => _objectsToActivate;
        public GameObject[] ObjectsToDeActivate => _objectsToDeActivate;
        public ParticleSystem[] ParticleSystemsToPlay => _particleSystemsToPlay;

        private void Awake()
        {
            TaskList.TaskReferencer = this;
        }
    }
}
