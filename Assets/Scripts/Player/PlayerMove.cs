using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    [SerializeField]
    private InputActionReference moveActionReference;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    private Animator animator;
    private bool isCurrentlyFrozen = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        if (!isCurrentlyFrozen)
        {
            rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * moveInput.normalized));
            animator.SetFloat("InputX", moveInput.normalized.x);
        }
    }

    private void FreezePosition(bool freeze)
    {
        isCurrentlyFrozen = freeze;

        if (freeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.linearVelocity = Vector2.zero;
            moveInput = Vector2.zero;
        } else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void OnMovePerformed(InputAction.CallbackContext value)
    {
        //Read value from control
        moveInput = value.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext value)
    {
        moveInput = Vector2.zero;
    }

    public void OnEnable()
    {
        moveActionReference.action.Enable();
        moveActionReference.action.performed += OnMovePerformed;
        moveActionReference.action.canceled += OnMoveCanceled;

        //DetectPlayer.OnChangeFreezeState += FreezePosition;
        DialogueManager.OnDialogueStateChange += FreezePosition;
        NoteBehaviour.OnNoteShowing += FreezePosition;
    }

    public void OnDisable()
    {
        moveActionReference.action.performed -= OnMovePerformed;
        moveActionReference.action.canceled -= OnMoveCanceled;

        //DetectPlayer.OnChangeFreezeState -= FreezePosition;
        DialogueManager.OnDialogueStateChange -= FreezePosition;
        NoteBehaviour.OnNoteShowing -= FreezePosition;

        moveActionReference.action.Disable();
    }
}
