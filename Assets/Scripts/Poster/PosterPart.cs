using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "PosterPart", menuName = "Scriptable Objects/Poster")]
public class PosterPart : ScriptableObject
{
    public Sprite Image;
    public bool IsPositionated;
}
