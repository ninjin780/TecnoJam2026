using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public BoxCollider2D mapBounds;

    private float minX, minY, maxX, maxY;
    private float camX, camY;
    private float camSize;
    private float camRatio;
    private Camera cam;

    private void Start()
    {
        minX = mapBounds.bounds.min.x;
        minY = mapBounds.bounds.min.y;
        maxX = mapBounds.bounds.max.x;
        maxY = mapBounds.bounds.max.y;

        cam = GetComponent<Camera>();
        camSize = cam.orthographicSize;
        camRatio = (maxX + camSize) / 2.0f;
    }

    void LateUpdate()
    {
        camY = Mathf.Clamp(target.position.y, minY + camSize, maxY - camSize);
        camX = Mathf.Clamp(target.position.x, minX + camSize, maxX - camSize);

        if (target != null)
        {
            transform.position = new Vector3(
                camX,
                camY,
                transform.position.z
                );
        }
    }
}
 