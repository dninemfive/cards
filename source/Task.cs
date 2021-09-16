using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace cards
{
    public class Task
    {
        #region variables
        public string Title = null, Content = null;
        public TaskState State = TaskState.INACTIVE;

        #region time
        public DateTime Deadline;
        public DateTime? SatisfiedUntil = null;
        public TimeSpan? PredictedDuration = null;
        public TimeSpan? FinalDuration = null;

        #region derived
        public TimeSpan TimeRemaining => Deadline - DateTime.Now;
        public bool TimedOut => TimeRemaining <= TimeSpan.Zero;
        #endregion derived

        #endregion time

        public int Weight { get; set; }
        public Color Color = Color.Gray;
        public int TimesRepeated = 0, TimesToRepeat = -1;
        public List<string> tags = new List<string>();
        #region metatasks
        public List<Task> Subtasks = new List<Task>();        
        public List<Task> Supertasks = new List<Task>();        
        public List<Task> Prerequisites = new List<Task>();
        #region derived
        public bool IsSupertask => Subtasks.Count == 0;
        public bool IsSubtask => Supertasks.Count == 0;
        public int PrerequisitesFulfilled => Prerequisites.Where(x => x.State == TaskState.COMPLETE || x.State == TaskState.SATISFIED).ToList().Count;
        #endregion derived
        #endregion metatasks
        #endregion variables
        public bool CanBeRandomlySelected
        {
            get
            {
                return State == TaskState.INACTIVE && !IsSupertask && PrerequisitesFulfilled == Prerequisites.Count;
            }
        }
        public static readonly Task Empty;
    }
    public enum TaskState
    {
        INACTIVE,   // The Task is not active and is only visible in the task list
        ACTIVE,     // The Task is active and is displayed on the details screen
        COMPLETE,   // The Task is fully complete
        ABANDONED,  // The Task has been removed from the list of incomplete tasks without being completed
        SATISFIED   // The Task is repeating with a delay between repeats and does not need to be completed until the SatisfiedUntil date.
    }
}
