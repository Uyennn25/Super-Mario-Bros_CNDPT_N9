
using System;
using System.Collections;
using System.Collections.Generic;
#if USE_UNITY_NOTIFICATION && UNITY_ANDROID
using Unity.Notifications.Android;
#endif
using UnityEngine;

public class MobileNotificationManager : MonoBehaviour
{
#if USE_UNITY_NOTIFICATION && UNITY_ANDROID

    [SerializeField] MobileNotificationConfig mobileNotificationConfig;
    void Start()
    {
        AndroidNotificationCenter.CancelAllNotifications();
        StartCoroutine(WaitForSetNotification());

    }
    IEnumerator WaitForSetNotification()
    {
        yield return new WaitForSecondsRealtime(3);
        var channel = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.Default,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(channel);

        foreach (NotificationDay notificationDay in mobileNotificationConfig.notificationDays)
        {
            foreach (NotificationContent notificationContent in notificationDay.NotificationContents)
            {
                DateTime notificationTime = DateTime.Now.Date.AddDays(notificationDay.day).AddHours(notificationContent.pushTime);
                AndroidNotification notification = new AndroidNotification()
                {
                    Title = notificationContent.title,
                    Text = notificationContent.text,
                    SmallIcon = notificationContent.smallIcon,
                    LargeIcon = notificationContent.largeIcon,
                    FireTime = notificationTime,
                };

                AndroidNotificationCenter.SendNotification(notification, "channel_id");
                Debug.Log("SendNotification: " + notificationContent.title + "-" + notificationContent.text + "-" + notificationTime);
            }
        }
    }
#endif
}


