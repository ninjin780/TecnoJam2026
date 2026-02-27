using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class PosterManager : MonoBehaviour
{
    public Image Image;

    private List<PosterSlot> posterParts;
    [SerializeField]
    public List<RectTransform> CorrectPositions;

    void Start()
    {
        posterParts = new List<PosterSlot>();
        foreach (RectTransform pos in CorrectPositions)
        {
            posterParts.Add(GetComponentInChildren<PosterSlot>());
        }
    }
}
