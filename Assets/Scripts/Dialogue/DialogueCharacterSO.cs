using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Character", menuName = "Scriptable Objects/Dialogue Character")]
public class DialogueCharacterSO : ScriptableObject
{
    [Header("Character Info")]
    public string CharacterName;
    public Sprite CharacterSprite;
}
