using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using System.Windows;

namespace cards
{
    public class Task
    {
        #region variables
        public string Title = null, Content = null;
        private TaskState _state = TaskState.INACTIVE;        
        public TaskState State
        {
            get => _state;
            set
            {
                _state = value;
            }
        }

        #endregion time

        public Color Color = Color.Gray;
        public bool CanBeRandomlySelected
        {
            get
            {
                return State.IsSelectable();
            }
        }
        public static readonly Task Empty = new Task() { Title = "Empty", Content = "Empty placeholder task." };
        public void Activate() { State = TaskState.ACTIVE; }
        public void Deactivate() { State = TaskState.INACTIVE; }
        public List<Task> Subtasks = new List<Task>();

        public override string ToString()
        {
            string str = "Task { " + Title + ", " + Content + " }";
            if(Subtasks.Count > 0)
            {
                str += "\n";
                foreach (Task t in Subtasks) str += t.ToString() + "\n";
            }
            return str;
        }
        public (string title, Span content) Details(int n = 0)
        {
            string tabs = "";
            for (int i = 0; i < n; i++) tabs += "\t";
            string title;
            title = tabs + Title;
            Span content = new Span();
            content.Inlines.Add(new Run(tabs + content));
            if (Subtasks.Count > 0)
            {
                content.Inlines.Add(new Run("\n\n" + tabs + "Subtasks:"));
                foreach (Task t in Subtasks)
                {
                    Span newSpan = new Span();
                    (string subtitle, Span subcontent) = t.Details(n + 1);
                    newSpan.Inlines.Add(new Run("\n" + subtitle + "\n") { FontWeight = FontWeights.Bold });
                    newSpan.Inlines.Add(subcontent);
                    content.Inlines.Add(newSpan);
                }
            }
            return (title, content);
        }
    }
    public enum TaskState
    {
        INACTIVE,   // The Task is not active and is only visible in the task list
        ACTIVE,     // The Task is active and is displayed on the details screen
        COMPLETE,   // The Task is fully complete
        ABANDONED,  // The Task has been removed from the list of incomplete tasks without being completed
        SATISFIED   // The Task is repeating with a delay between repeats and does not need to be completed until the SatisfiedUntil date.
    }
    public static class TaskStateExtensions
    {
        public static bool IsActive(this TaskState state)
        {
            return state == TaskState.ACTIVE;
        }
        public static bool WasSuccessful(this TaskState state)
        {
            return state == TaskState.COMPLETE || state == TaskState.SATISFIED;
        }
        public static bool IsSelectable(this TaskState state)
        {
            return state == TaskState.INACTIVE;
        }
    }
}
