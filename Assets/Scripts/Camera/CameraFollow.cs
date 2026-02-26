using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new(0, 0, -10);

    Vector2 pos;
    Vector2 targetPos;

    InputAction moveAction;

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Car/Move");
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = new Vector3(
                transform.position.x,
                target.position.y + offset.y,
                offset.z
                );
        }
    }
}
 