using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cards
{
    public static class TaskManager
    {
        public static List<Task> AllTasks = new List<Task>();
        public static List<Task> TasksWhichAre(TaskState s) => AllTasks.Where(x => x.State == s).ToList();
        public static List<Task> InactiveTasks => TasksWhichAre(TaskState.INACTIVE);
        public static Task RandomTask
        {
            get
            {
                List<Task> selectable = AllTasks.Where(x => x.CanBeRandomlySelected).ToList();
                if (selectable.Count == 0) return Task.Empty;
                int ind = new Random().Next(0, selectable.Count);
                return selectable[ind];
            }
        }
    }
}
