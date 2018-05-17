using System;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(WikiLanglinks.iOS.Services.Alert))]
namespace WikiLanglinks.iOS.Services
{
    public class Alert : IAlert
    {
        private static readonly TimeSpan ShortDelay = TimeSpan.FromSeconds(2.0);
        private static readonly TimeSpan LongDelay = TimeSpan.FromSeconds(3.5);

        NSTimer alertDelay;
        UIAlertController alert;        
        
        public void Short(string message)
        {
            ShowAlert(message, ShortDelay);
        }

        public void Long(string message)
        {
            ShowAlert(message, LongDelay);
        }

        private void ShowAlert(string message, TimeSpan delay)
        {
            alertDelay = NSTimer.CreateScheduledTimer(delay, obj => DismissMessage());
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }
        
        private void DismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}
