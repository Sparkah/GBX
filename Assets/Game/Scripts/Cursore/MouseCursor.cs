using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(SpriteRenderer))]
public class MouseCursor : MonoBehaviour
{
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

    [SerializeField]
    private Vector2 spriteOffsetImage = new Vector2(0.8f, -.5f);

    private Transform _cursorTransform;

#if UNITY_EDITOR

    [SerializeField]
    private bool showCursorDebug = false;

    private void OnValidate()
    {
        if (Application.IsPlaying(this))
        {
            Cursor.visible = showCursorDebug;
        }
    }

#endif

    private void Reset()
    {
        Init();
    }

    private void Awake()
    {
        Cursor.visible = false;
        Init();
    }

    private void Start()
    {
        _cursorTransform.position = Vector3.forward * fixedOffsetZ;
        spriteRenderer.sprite = normalCursor;
    }

    private void Init()
    {
        spriteRenderer = spriteRenderer == null ? GetComponent<SpriteRenderer>() : spriteRenderer;
        playerCamera = playerCamera == null ? Camera.main : playerCamera;
        _cursorTransform = transform;
    }

    private void Update()
    {
        _cursorTransform.position = (Vector2)playerCamera.ScreenToWorldPoint(Input.mousePosition) + spriteOffsetImage;
    }
}