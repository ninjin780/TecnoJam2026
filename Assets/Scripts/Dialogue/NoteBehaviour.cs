using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer showInteractable;
    public bool IsPlayerInRange;

    private InputAction interaction;
    private InputAction escape;

    private SpriteRenderer spriteRenderer;

    private bool IsNoteShowing = false;
    public static event Action<bool> OnNoteShowing;

    private void Start()
    {
        interaction = InputSystem.actions.FindAction("Player/Interact");
        escape = InputSystem.actions.FindAction("Player/Escape");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (interaction.WasPressedThisFrame() && IsPlayerInRange)
        {
            SetSpriteRenderer(true);

            OnNoteShowing?.Invoke(true);
            IsNoteShowing = true;

            showInteractable.enabled = false;
        }

        if (IsNoteShowing)
        {
            if (escape.WasPressedThisFrame())
            {
                SetSpriteRenderer(false);
                OnNoteShowing?.Invoke(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsPlayerInRange = true;
        showInteractable.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerInRange = false;
        showInteractable.enabled = false;
    }

    private void SetSpriteRenderer(bool state)
    {
        spriteRenderer.enabled = state;
    }
}
