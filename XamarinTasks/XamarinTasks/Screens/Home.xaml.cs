using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTasks.Models;

namespace XamarinTasks.Screens
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();

            var date = DateTime.Now.ToString("dddd, dd {0} MMMM {0} yyyy");

            TodayDate.Text = string.Format(date, "of");

            LoadTasks();
        }

        public void StackLayoutLine(Task task, int index)
        {
            View centralStack;

            if (task.FinishedTime == null)
            {
                centralStack = new Label
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = task.Name,
                    TextColor = Color.Black
                };
            }
            else
            {
                centralStack = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Spacing = 0,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                ((StackLayout) centralStack).Children.Add(new Label
                {
                    Text = task.Name,
                    TextColor = Color.Gray
                });
                ((StackLayout) centralStack).Children.Add(new Label
                {
                    Text = "Finished on " + task.FinishedTime?.ToString("MM/dd/yyyy - hh:mm") + "h",
                    TextColor = Color.Gray,
                    FontSize = 10
                });
            }

            var priority = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile(Device.RuntimePlatform == Device.UWP
                    ? "Resources/" + task.Priority.ToString().ToLower() + ".png"
                    : task.Priority.ToString().ToLower() + ".png")
            };

            var check = new Image
            {
                VerticalOptions = LayoutOptions.Center
            };

            if (task.FinishedTime != null)
                check.Source = ImageSource.FromFile(Device.RuntimePlatform == Device.UWP
                    ? "Resources/CheckOn.png"
                    : "CheckOn.png");
            else
                check.Source = ImageSource.FromFile(Device.RuntimePlatform == Device.UWP
                    ? "Resources/CheckOff.png"
                    : "CheckOff.png");

            var checkTap = new TapGestureRecognizer();
            checkTap.Tapped += delegate
            {
                new TaskManager().FinalizeTask(task, index);
                LoadTasks();
            };

            check.GestureRecognizers.Add(checkTap);

            var delete = new Image
            {
                VerticalOptions = LayoutOptions.Center,
                Source = ImageSource.FromFile(Device.RuntimePlatform == Device.UWP
                    ? "Delete.png"
                    : "Resources/Delete.png")
            };

            var deleteTap = new TapGestureRecognizer();
            deleteTap.Tapped += delegate
            {
                new TaskManager().RemoveTask(index);
                LoadTasks();
            };
            delete.GestureRecognizers.Add(deleteTap);

            var line = new StackLayout {Orientation = StackOrientation.Horizontal, Spacing = 15};

            line.Children.Add(check);
            line.Children.Add(centralStack);
            line.Children.Add(priority);
            line.Children.Add(delete);

            SlTasks.Children.Add(line);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Register());
        }

        private void LoadTasks()
        {
            SlTasks.Children.Clear();

            var taskList = new TaskManager().Listing();

            var index = 0;

            foreach (var task in taskList)
            {
                StackLayoutLine(task, index);

                index++;
            }
        }
    }
}