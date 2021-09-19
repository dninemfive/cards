using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization;

namespace cards
{
    public static class TaskManager
    {
        public const string TaskPath = @"C:\Users\dninemfive\Documents\notes\md\tasks2.yml";
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
        public static void LoadTasks()
        {
            string yml = System.IO.File.ReadAllText(TaskPath);
            Deserializer deserializer = new Deserializer(); 
            TaskList list = deserializer.Deserialize<TaskList>(yml);
            foreach(Task t in list.tasks)
            {
                AllTasks.Add(t);
            }
        }
    }
    public class TaskList { public List<Task> tasks;  }
}
