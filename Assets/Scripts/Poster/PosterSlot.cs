using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PosterSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private ObjectPosterPart poster;
    private Sprite image;
    private Vector3 originalPosition;
    private Transform parent;
    private Canvas canvas;
    private Image uiImage;

    public static event Action CorrectDrop;

    private void Start()
    {
        parent = transform.parent;
        canvas = GetComponentInParent<Canvas>();
        poster = GetComponentInChildren<ObjectPosterPart>();
        uiImage = GetComponent<Image>();

        if (poster)
        {
            image = poster.GetPosterPart().Image;
            uiImage.sprite = image;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Store previous reference position
        parent = transform.parent;
        originalPosition = transform.position;

        // Change parent of our item to the canvas
        transform.SetParent(canvas.transform, true);

        // And set it as last child to be rendered on top of UI
        transform.SetAsLastSibling();

        uiImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Moving object around screen using mouse delta
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameObject hitData = eventData.pointerCurrentRaycast.gameObject;

        if (hitData)
        {
            RectTransform hitRect = hitData.GetComponent<RectTransform>();
            RectTransform targetRect = poster.GetCorrectPosition();

            if (hitRect != null && hitRect == targetRect)
            {

                transform.SetParent(hitRect);

                transform.position = hitRect.position;

                poster.GetPosterPart().IsPositionated = true;
                CorrectDrop?.Invoke();

                return; 
            }
        }

        uiImage.raycastTarget = true;
        transform.SetParent(parent);
        transform.position = originalPosition;
    }
}
