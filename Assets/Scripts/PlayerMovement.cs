using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float Speed = 5.0f;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() // Mejor usar FixedUpdate para físicas
    {
        rb.linearVelocity = moveInput * Speed;

        if (moveInput != Vector2.zero)
        {
           
            if (moveInput.x != 0)
            {
                transform.localScale = new Vector3(-Mathf.Sign(moveInput.x), 1, 1);
            }
            animator.SetFloat("horizontal", moveInput.x);
            animator.SetFloat("vertical", moveInput.y);
            animator.SetBool("isMoving", true);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
