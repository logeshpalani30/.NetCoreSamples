using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using AndroidX.Core.App;
using Firebase.Messaging;
using System;
using System.Text.RegularExpressions;
using Debug = System.Diagnostics.Debug;

namespace FcmTestApp.Services
{
    [Service][BroadcastReceiver]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class FcmService: FirebaseMessagingService
    {
        public const string PRIMARY_CHANNEL = "default";
        public NotificationCompat.Builder notification;

        public override void OnNewToken(string p0)
        {
            base.OnNewToken(p0);

            Debug.WriteLine(p0);
        }

        public override void OnMessageReceived(RemoteMessage p0)
        {
            base.OnMessageReceived(p0);

            var intent = new Intent(this.ApplicationContext, typeof(MainActivity));

            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
            var seed = Convert.ToInt32(Regex.Match(Guid.NewGuid().ToString(), @"\d+").Value);
            var id = new Random(seed).Next(000000000, 999999999);
            var fullScreenPendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.CancelCurrent);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var chennel = new NotificationChannel(PRIMARY_CHANNEL, new Java.Lang.String("Primary"), NotificationImportance.High);
                chennel.LightColor = Color.Green;
                manager.CreateNotificationChannel(chennel);
                this.notification = new NotificationCompat.Builder(this, PRIMARY_CHANNEL);
            }
            else
            {
                this.notification = new NotificationCompat.Builder(this);
            }

            this.notification.SetContentIntent(fullScreenPendingIntent)
                .SetContentTitle(p0.GetNotification().Title)
                .SetContentText(p0.GetNotification().Body)
                .SetLargeIcon(BitmapFactory.DecodeResource(base.Resources, Resource.Drawable.ic_launcher))
                .SetSmallIcon(Resource.Drawable.ic_launcher)
                .SetStyle((new NotificationCompat.BigTextStyle()))
                .SetPriority(NotificationCompat.PriorityHigh)
                .SetColor(0x9c6114)
                .SetAutoCancel(true);
            manager.Notify(id, this.notification.Build());
        }

        public override void OnDeletedMessages()
        {
            base.OnDeletedMessages();
        }
    }
}