using UnityEngine;

[CreateAssetMenu(fileName = "NewDialogue", menuName = "Dialogue")]
public class Lines : ScriptableObject
{
    [TextArea(4, 6)]
    public string[] DialogueLines;
}
