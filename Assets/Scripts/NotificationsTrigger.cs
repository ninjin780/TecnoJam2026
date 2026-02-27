using UnityEngine;

public class NotificationsTrigger : MonoBehaviour
{
    public NotificationSO notificationData;

    private void Start()
    {
        if (NotificatinosManager.instance != null)
        {
            NotificatinosManager.instance.ShowNotification(notificationData);
        }

        gameObject.SetActive(false);
    }
}
