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
        [SerializeField] private ParticleSystem _ps;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private bool _isDead;
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

        public override void PlayerDeathAnim()
        {
            if(_isDead) return;

            _isDead = true;
            _rigidbody2D.gravityScale = 0;
            var colliders = GetComponentsInChildren<BoxCollider2D>();
            foreach (var collider2D1 in colliders)
            {
                collider2D1.enabled = false;
            }

            StartCoroutine(DieMouse());
        }

        private IEnumerator DieMouse()
        {
            _ps.Play();
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(_waitTime);
            gameObject.SetActive(false);
        }
    }
}
