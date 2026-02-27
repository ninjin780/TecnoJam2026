using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificatinosManager : MonoBehaviour
{
    public static NotificatinosManager instance;

    [Header("Notification UI")]
    [SerializeField] private GameObject NotificationParent;
    [SerializeField] private TMP_Text NotificationTextUI;

    private CanvasGroup notificationsUICanvasGroup;
    private Queue<NotificationSO> notificationsQueue = new();
    private bool isDisplayingNotification = false;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else Destroy(gameObject);

        notificationsUICanvasGroup = NotificationParent.GetComponent<CanvasGroup>();
        notificationsUICanvasGroup.alpha = 0.0f;
    }

    public void ShowNotification(NotificationSO notification)
    {
        notificationsQueue.Enqueue(notification);

        if (!isDisplayingNotification)
        {
            StartCoroutine(DisplayNotification());
        }
    }

    private IEnumerator DisplayNotification()
    {
        isDisplayingNotification = true;

        while (notificationsQueue.Count > 0)
        {
            NotificationSO data = notificationsQueue.Dequeue();

            NotificationTextUI.text = data.Notification;

            yield return StartCoroutine(FadeCanvasGroup(notificationsUICanvasGroup, true, data.FadeDuration));

            yield return new WaitForSeconds(data.TextDuration);

            yield return StartCoroutine(FadeCanvasGroup(notificationsUICanvasGroup, false, data.FadeDuration));
        }

        isDisplayingNotification = false;
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, bool fadeIn,  float duration)
    {
        float targetAlpha = fadeIn ? 1.0f : 0.0f;
        float initAlpha = canvasGroup.alpha;
        float timer = 0.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(targetAlpha, initAlpha, timer / duration);
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;
    }
}
