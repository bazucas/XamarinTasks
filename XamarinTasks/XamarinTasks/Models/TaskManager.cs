using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Newtonsoft.Json;

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

        public void RemoveTask(int index)
        {
            List = Listing();
            List.RemoveAt(index);

            SaveInProperties(List);
        }

        public void FinalizeTask(Task task, int index)
        {
            List = Listing();
            List.RemoveAt(index);
            task.FinishedTime = DateTime.Now;
            List.Add(task);
            SaveInProperties(List);
        }

        public List<Task> Listing()
        {
            return ListInProperties();
        }

        private static void SaveInProperties(List<Task> list)
        {
            if (Application.Current.Properties.ContainsKey("Tasks"))
            {
                Application.Current.Properties.Remove("Tasks");
            }

            var jsonVal = JsonConvert.SerializeObject(list);

            Application.Current.Properties.Add("Tasks", jsonVal);
        }

        private static List<Task> ListInProperties()
        {
            if (!Application.Current.Properties.ContainsKey("Tasks")) return new List<Task>();

            var jsonVal = (string)Application.Current.Properties["Tasks"];

            var lista = JsonConvert.DeserializeObject<List<Task>>(jsonVal);

            return lista;
        }
    }
}