using System;
using Android.Widget;
using Xamarin.Forms;

[assembly: Dependency(typeof(WikiLanglinks.Droid.Services.Alert))]
namespace WikiLanglinks.Droid.Services
{    
    public class Alert : IAlert
    {
        public void Short(string message)
        {
            ShowAlert(message, ToastLength.Short);
        }

        public void Long(string message)
        {
            ShowAlert(message, ToastLength.Long);
        }

        private void ShowAlert(string message, ToastLength delay)
        {
            var toast = Toast.MakeText(Android.App.Application.Context, message, delay);
            toast.Show();
        }
    }
}
