using UnityEngine;

namespace Game.Tasks
{
    [CreateAssetMenu(fileName = "New task list", menuName = "GBX/Task list", order = 1)]
    public class TaskListSO : ScriptableObject
    {
        public TaskSO[] Tasks;

        [SerializeField] private GameObject[] _objectsToActivate;
        [SerializeField] private GameObject[] _objectsToDeActivate;
        [SerializeField] private ParticleSystem[] _particleSystemsToPlay;

        public GameObject[] ObjectsToActivate => _objectsToActivate;
        public GameObject[] ObjectsToDeActivate => _objectsToDeActivate;
        public ParticleSystem[] ParticleSystemsToPlay => _particleSystemsToPlay;
    }
}
