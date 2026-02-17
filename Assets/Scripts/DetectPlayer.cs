using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectPlayer : MonoBehaviour
{
    public GameObject ActionObject;
    public GameObject CleaningText;
    public bool IsPlayerInRange = false;

    private InputAction interactAction;
    private SpriteRenderer spriteRenderer;
    private bool rendererActive = true;

    [SerializeField] private float coolDown = 1.5f;
    private float timer = 0.0f;
    private bool isCoolDownDone = false;
    private bool canDeleteObject = false;

    public static event Action<bool> OnChangeFreezeState;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Player/Interact");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (interactAction.WasPressedThisFrame() && IsPlayerInRange)
        {
            canDeleteObject = true;
            CleaningText.SetActive(true);

            OnChangeFreezeState?.Invoke(true);
        }

        if (canDeleteObject)
        {
            timer += Time.deltaTime;
            Debug.Log(timer);

            if (timer >= coolDown) isCoolDownDone = true;

            if (isCoolDownDone)
            {
                DeactivateElements();
            }
        }
    }

    private void DeactivateElements()
    {
        spriteRenderer.enabled = false;
        rendererActive = false;

        ActionObject.SetActive(false);
        CleaningText.SetActive(false);

        isCoolDownDone = false;
        timer = 0.0f;
        canDeleteObject = false;

        OnChangeFreezeState?.Invoke(false);
    }

    private void OnTriggerEnter2D()
    {
        if (rendererActive)
        {
            ActionObject.SetActive(true);
        }

        IsPlayerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ActionObject.SetActive(false);
        IsPlayerInRange = false;
    }
}
