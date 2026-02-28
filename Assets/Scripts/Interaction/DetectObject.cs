using UnityEngine;

public class DetectObject : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5.0f;

    private void Update()
    {
        DetectObjects();
    }

    private void DetectObjects()
    {
        RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, transform.forward, detectionRange);

        foreach(RaycastHit hit in raycastHits)
        {
            Debug.Log(hit);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
