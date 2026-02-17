using System;
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        rb.MovePosition(rb.position + (speed * Time.fixedDeltaTime * moveInput.normalized));
    }

    private void FreezePosition(bool freeze)
    {
        if (freeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        } else
        {
            rb.constraints = RigidbodyConstraints2D.None;
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

        DetectPlayer.OnChangeFreezeState += FreezePosition;
    }

    public void OnDisable()
    {
        moveActionReference.action.performed -= OnMovePerformed;
        moveActionReference.action.canceled -= OnMoveCanceled;
        moveActionReference.action.Disable();

        DetectPlayer.OnChangeFreezeState -= FreezePosition;
    }
}
