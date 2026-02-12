using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float AccelerationFactor = 30.0f;
    public float TurnFactor = 3.5f;
    public float DriftFactor = 0.95f;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyEngineForce();

        RemoveSideVelocity();

        ApplySteering();
    }

    void ApplyEngineForce()
    {
        //Create force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * AccelerationFactor;

        //Apply force and gets the car forward
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Rotate car
        rb.MoveRotation(rb.rotation - (steeringInput * TurnFactor));
    }

    void RemoveSideVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.linearVelocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.linearVelocity, transform.right);

        rb.linearVelocity = (forwardVelocity + rightVelocity) * DriftFactor;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        accelerationInput = input.y;
        steeringInput = input.x;

        if (context.canceled)
        {
            accelerationInput = 0f;
            steeringInput = 0f;
        }
    }
}