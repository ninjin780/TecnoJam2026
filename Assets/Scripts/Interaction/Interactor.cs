using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    public Transform Player;
    public float InteractionRange;

    public InputAction inputAction;

    private void Start()
    {
        inputAction = InputSystem.actions.FindAction("Player/Interact");
    }

    private void Update()
    {
        if (inputAction.WasPerformedThisFrame())
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, InteractionRange);

            foreach(Collider2D hit in hits)
            {
                if (hit.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact();
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InteractionRange);
    }
}
