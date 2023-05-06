using System;
using System.Collections;
using UnityEngine;

namespace Game.Scripts.TaskAnimatedCharacters
{
    public class VaseAnimat : ParentAnima
    {
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _waitBeforeDisappear = 1f;
        public override void PlayerDeathAnim()
        {
            //Collision with player detecteed here
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("TaskKilla"))
            {
                Debug.Log("vase hit the ground");
                _collider.enabled = true;
                StartCoroutine(KillObject());
            }
        }

        private IEnumerator KillObject()
        {
            yield return new WaitForSeconds(_waitBeforeDisappear);
            gameObject.SetActive(false);
        }
    }
}
