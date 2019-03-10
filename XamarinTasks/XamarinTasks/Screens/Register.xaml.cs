using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTasks.Models;

namespace XamarinTasks.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Register : ContentPage
    {
        private PriorityEnum Priority { get; set; }

        public Register()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var stacks = SlPriorities.Children;

            foreach (var stack in stacks)
            {
                if ((stack as StackLayout)?.Children[1] is Label lblPriority) lblPriority.TextColor = Color.Gray;
            }

            ((Label)((StackLayout)sender).Children[1]).TextColor = Color.Black;
            var source = ((Image) ((StackLayout) sender).Children[0]).Source as FileImageSource;
            if (source == null) return;
            var priority = source.File.Replace("Resources/", "").Replace(".png", "");
            Enum.TryParse(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(priority.ToLower()), out PriorityEnum parsedEnum);
            Priority = parsedEnum;
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            var existingError = false;
            if (!(TxtName.Text.Trim().Length > 0))
            {
                existingError = true;
                DisplayAlert("Error", "Unfilled name", "Ok");
            }

            if (!(Priority > 0))
            {
                existingError = true;
                DisplayAlert("Error", "Unfilled priority", "Ok");
            }

            if (existingError) return;
            var task = new Task {Name = TxtName.Text.Trim(), Priority = Priority};
            var taskMan = new TaskManager();
            taskMan.SaveTask(task);

            Application.Current.MainPage = new NavigationPage(new Home());
        }
    }
}