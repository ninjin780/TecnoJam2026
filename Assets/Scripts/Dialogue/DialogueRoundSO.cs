using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Scriptable Objects/Dialogue Round")]
public class DialogueRoundSO : ScriptableObject
{
    public List<DialogueTurn> dialogueTurnList;
}
