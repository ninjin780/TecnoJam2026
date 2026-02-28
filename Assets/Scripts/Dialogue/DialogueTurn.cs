using UnityEngine;

[System.Serializable]
public class DialogueTurn
{
    public DialogueCharacterSO CharacterSO;
    [TextArea(2, 4)] public string DialogueLine = string.Empty;
}
