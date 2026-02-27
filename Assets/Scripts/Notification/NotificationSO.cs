using UnityEngine;

[CreateAssetMenu(fileName = "NewNotification", menuName = "Notifications")]
public class NotificationSO : ScriptableObject
{
    [TextArea] public string Notification;
    public float TextDuration = 2.0f;
    public float FadeDuration = 1.0f;
}
