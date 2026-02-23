using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DetectPlayer : MonoBehaviour
{
    public GameObject ActionObject;
    public GameObject CleaningObject;
    public GameObject CleaningBg;
    public TextMeshProUGUI cleaningText;

    public bool IsPlayerInRange = false;

    private InputAction interactAction;
    private SpriteRenderer spriteRenderer;
    private bool rendererActive = true;

    [SerializeField] private float coolDown = 1.5f;
    private float timer = 0.0f;
    private bool isCoolDownDone = false;
    private bool canDeleteObject;

    public static event Action<bool> OnChangeFreezeState;

    private void Start()
    {
        interactAction = InputSystem.actions.FindAction("Player/Interact");
        spriteRenderer = GetComponent<SpriteRenderer>();
        cleaningText = CleaningObject.GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (interactAction.WasPressedThisFrame() && IsPlayerInRange)
        {
            canDeleteObject = true;
            CleaningObject.SetActive(true);
            CleaningBg.SetActive(true);

            OnChangeFreezeState?.Invoke(true);
        }

        if (canDeleteObject)
        {
            timer += Time.deltaTime;
            AnimateText();

            if (timer >= coolDown) isCoolDownDone = true;

            if (isCoolDownDone)
            {
                DeactivateElements();
            }
        }
    }

    private void AnimateText()
    {
        string text = "Cleaning";

        text += ".";

        cleaningText.text = text;
    }

    private void DeactivateElements()
    {
        spriteRenderer.enabled = false;
        rendererActive = false;

        ActionObject.SetActive(false);
        CleaningObject.SetActive(false);
        CleaningBg.SetActive(false);

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
