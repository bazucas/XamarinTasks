using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinTasks.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }
    }
}