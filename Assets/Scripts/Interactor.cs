using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 3f;
    [SerializeField] private InputActionReference interactInput;

    private void OnEnable()
    {
        // 1. Tell the Input System to start listening to this specific action
        interactInput.action.Enable();

        // 2. Manually link your function to the 'performed' event
        interactInput.action.performed += OnInteractPerformed;
    }

    private void OnDisable()
    {
        // 3. Clean up so you don't get errors when the object is destroyed
        interactInput.action.performed -= OnInteractPerformed;
        interactInput.action.Disable();
    }
    public void OnInteractPerformed(InputAction.CallbackContext value)
    {
        Debug.Log("Trying to interact");
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactionRange);

        Collider2D closest = null;
        float closestDist = Mathf.Infinity;

        foreach (var hit in hits)
        {
            if (!hit.TryGetComponent<IInteractable>(out _)) continue;

            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < closestDist)
            {
                closest = hit;
                closestDist = dist;
            }
        }

        if (closest != null && closest.TryGetComponent<IInteractable>(out var objeto))
        {
            objeto.Interact();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
