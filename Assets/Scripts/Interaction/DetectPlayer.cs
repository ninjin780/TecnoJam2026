using UnityEngine;
using UnityEngine.InputSystem;

public class CleaningBehaviour : MonoBehaviour
{
    [SerializeField] private SpriteRenderer showInteractable;
    public bool IsPlayerInRange = false;

    private SpriteRenderer currentSprite;
    [SerializeField] private Sprite newSprite;
    public bool HasLitUpCandle = false;

    private InputAction interaction;

    private void Start()
    {
        interaction = InputSystem.actions.FindAction("Player/Interact");
        currentSprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (interaction.WasPressedThisFrame() && IsPlayerInRange)
        {
            if (gameObject.CompareTag("Dust")) Destroy(gameObject);

            if (gameObject.CompareTag("Candle") && !HasLitUpCandle)
            {
                currentSprite.sprite = newSprite;
                HasLitUpCandle = true;
            }
        }
    }

    private void OnTriggerEnter2D()
    {
        if (gameObject.CompareTag("Candle") && !HasLitUpCandle || gameObject.CompareTag("Dust"))
        {
            IsPlayerInRange = true;
            if (showInteractable != null) showInteractable.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerInRange = false;
        if (showInteractable != null) showInteractable.enabled = false;
    }
}
