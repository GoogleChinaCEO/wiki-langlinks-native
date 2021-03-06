﻿using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
			MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
			((MainPage as NavigationPage).CurrentPage as MainPage).Init();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
