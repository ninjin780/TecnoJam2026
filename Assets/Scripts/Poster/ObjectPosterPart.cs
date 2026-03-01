using UnityEngine;

public class ObjectPosterPart : MonoBehaviour
{
    [SerializeField] private PosterPart part;
    [SerializeField] private RectTransform correctPosition;

    private void Start()
    {
        part.IsPositionated = false;
    }

    public PosterPart GetPosterPart() 
    { 
        return part; 
    }

    public RectTransform GetCorrectPosition()
    {
        return correctPosition;
    }


}
