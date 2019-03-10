using System.Collections.Generic;
using Xamarin.Forms;

namespace XamarinTasks.Models
{
    public class TaskManager
    {
        public List<Task> List { get; set; }

        public void SaveTask(Task task)
        {
            List = Listing();
            List.Add(task);

            SaveInProperties(List);
        }

        public void RemoveTask(Task task)
        {
            List = Listing();
            List.Remove(task);

            SaveInProperties(List);
        }

        public void FinalizeTask(int index, Task task)
        {
            List.RemoveAt(index);
            List.Add(task);
            SaveInProperties(List);
        }

        public List<Task> Listing()
        {
            return ListInProperties();
        }

        private static void SaveInProperties(List<Task> list)
        {
            if (Application.Current.Properties.ContainsKey("Tasks")) Application.Current.Properties.Remove("Tasks");
            Application.Current.Properties.Add("Tasks", list);
        }

        private static List<Task> ListInProperties()
        {
            if (Application.Current.Properties.ContainsKey("Tasks"))
                return (List<Task>) Application.Current.Properties["Tasks"];
            return new List<Task>();
        }
    }
}