using UnityEngine;

public class ObjectPosterPart : MonoBehaviour
{
    [SerializeField] private PosterPart part;


    public PosterPart GetPosterPart() 
    { 
        return part; 
    }

}
