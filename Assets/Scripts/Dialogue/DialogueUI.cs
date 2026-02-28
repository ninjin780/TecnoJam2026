using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [Header("Dialogue Parameters")]
    [SerializeField] private RectTransform dialogueBox;
    [SerializeField] private TextMeshProUGUI dialogueAreaText;

    [Header("Character Parameters")]
    [SerializeField] private Image characterPhoto;
    [SerializeField] private TextMeshProUGUI characterName;

    public void ChangeDialogueBoxState(bool state)
    {
        dialogueBox.gameObject.SetActive(state);
    }

    public void SetCharacterInfo(DialogueCharacterSO character)
    {
        if (character == null) return;

        characterPhoto.sprite = character.CharacterSprite;
        characterName.text = character.CharacterName;
    }

    public void SetDialogueTextArea(string text)
    {
        dialogueAreaText.text = text;
    }

    public void ClearDialogueTextArea()
    {
        dialogueAreaText.text = string.Empty;
    }

    public void TypeInDialogueArea(char letter)
    {
        dialogueAreaText.text += letter;
    }
}
