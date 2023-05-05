using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Cursor
{
    public class MouseCursor : MonoBehaviour
    {
        [SerializeField]
        private Transform cursorToMove;

        [Header("Sprites")]
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private Sprite normalCursor;

        [Header("Camera")]
        [SerializeField]
        private Camera playerCamera;

        [SerializeField]
        private float fixedOffsetZ = -10;

#if UNITY_EDITOR

        [SerializeField]
        private bool showCursorDebug = false;

        private void OnValidate()
        {
            if (Application.IsPlaying(this))
            {
                UnityEngine.Cursor.visible = showCursorDebug;
            }
        }

#endif

        private void Reset()
        {
            Init();
        }

        private void Awake()
        {
            UnityEngine.Cursor.visible = false;
            Init();
        }

        private void Start()
        {
            cursorToMove.position = Vector3.forward * fixedOffsetZ;
            spriteRenderer.sprite = normalCursor;
        }

        private void Init()
        {
            spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;
            playerCamera = playerCamera == null ? Camera.main : playerCamera;
            cursorToMove = cursorToMove == null ? transform : cursorToMove;
        }

        private void Update()
        {
            cursorToMove.position = (Vector2)playerCamera.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}