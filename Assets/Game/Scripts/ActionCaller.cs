using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Assets.Game.Scripts
{
    public class ActionCaller : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent onCallAction;

        public void DoAction()
        {
            onCallAction?.Invoke();
        }
    }
}