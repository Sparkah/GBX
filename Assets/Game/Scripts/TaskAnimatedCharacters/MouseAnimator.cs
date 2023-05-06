using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.TaskAnimatedCharacters
{
    public class MouseAnimator : ParentAnima
    {
        [SerializeField] private float _waitTime = 1f;
        private Rigidbody2D _rigidbody2D;
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            StartCoroutine(DumbAssAI());
        }

        private IEnumerator DumbAssAI()
        {
            gameObject.transform.DOScale(new Vector3(0.11f, 0.11f, 0.11f), 0.45f)
                .OnComplete(() =>
                {
                    gameObject.transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.45f)
                        .OnComplete(() =>
                        {
                        });
                });
            yield return new WaitForSeconds(_waitTime);
            _rigidbody2D.AddForce(new Vector2(Random.Range(-3,4), Random.Range(0,6)), ForceMode2D.Impulse);
            StartCoroutine(DumbAssAI());
        }

        public override void PlayeDeathAnima()
        {
            _rigidbody2D.gravityScale = 0;
            var colliders = GetComponentsInChildren<Collider2D>();
            foreach (var collider2D1 in colliders)
            {
                collider2D1.enabled = false;
            }

        }
    }
}
