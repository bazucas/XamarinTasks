using System;

namespace XamarinTasks.Models
{
    public class Task
    {
        public string Name { get; set; }
        public DateTime? FinishedTime { get; set; }
        public PriorityEnum Priority { get; set; }
    }
}