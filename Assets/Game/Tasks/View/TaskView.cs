using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tasks.View
{
    public class TaskView : MonoBehaviour
    {
        public TextMeshProUGUI Text;
        public Image Image;

        [SerializeField] private bool _animate = true;
        [SerializeField] private float scaleAmount = 1.1f;
        [SerializeField] private float moveAmount = 10f;
        [SerializeField] private float animationDuration = 0.5f;

        private RectTransform rectTransform;
        private Vector3 originalPosition;
        private Vector3 originalScale;

        private Tweener scaleTween;
        private Tweener moveTween;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        void OnEnable()
        {
            if (!_animate) return;
            originalPosition = rectTransform.localPosition;
            originalScale = rectTransform.localScale;

            // Animate scale
            scaleTween = rectTransform.DOScale(scaleAmount * originalScale, animationDuration).SetLoops(-1, LoopType.Yoyo);
        
            // Animate position
            moveTween = rectTransform.DOLocalMoveY(originalPosition.y + moveAmount, animationDuration).SetLoops(-1, LoopType.Yoyo);
        }

        void OnDisable()
        {
            if (!_animate) return;
            // Reset the UI element to its original position and scale
            if (scaleTween != null) scaleTween.Kill();
            if (moveTween != null) moveTween.Kill();

            rectTransform.localPosition = originalPosition;
            rectTransform.localScale = originalScale;
        }
    }
}
