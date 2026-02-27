using UnityEngine;
using UnityEngine.EventSystems;

public class PosterSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ObjectPosterPart poster;
    private Transform parent;
    private Canvas canvas;

    private void Start()
    {
        parent = transform.parent;
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store previous reference position
        parent = transform.parent;

        // Change parent of our item to the canvas
        transform.SetParent(canvas.transform, true);

        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Find scene objects colliding with mouse point on end dragging
        GameObject hitData = eventData.pointerCurrentRaycast.gameObject;

        if (hitData)
        {

        }
    }
}
