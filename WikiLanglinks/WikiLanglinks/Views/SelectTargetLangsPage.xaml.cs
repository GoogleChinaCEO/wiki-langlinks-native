using System;
using Xamarin.Forms;

namespace WikiLanglinks
{
    public partial class SelectTargetLangsPage : ContentPage
    {
        public SelectTargetLangsPage()
        {
            InitializeComponent();
			BindingContext = new SelectTargetLangsViewModel();
        }
    }
}
