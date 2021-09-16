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
        public List<(DateTime stamp, TaskState state)> history = new List<(DateTime stamp, TaskState state)>();
        #region derived
        public TimeSpan TimeRemaining => Deadline - DateTime.Now;
        public bool TimedOut => TimeRemaining <= TimeSpan.Zero;
        private TimeSpan? _duration = null;
        public TimeSpan Duration
        {
            get
            {
                if (_duration != null) return (TimeSpan)_duration;
                if (history.Count == 0) return TimeSpan.Zero;
                TimeSpan ct = TimeSpan.Zero;
                for(int i = 0; i < history.Count - 1; i++)
                {
                    (DateTime stamp, TaskState state) curEntry = history[i], nextEntry = history[i + 1];
                    if (curEntry.state.IsActive()) ct += nextEntry.stamp - curEntry.stamp;
                }
                _duration = ct;
                return ct;
            }
        }
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
        public int PrerequisitesFulfilled => Prerequisites.Where(x => x.State.WasSuccessful()).ToList().Count;
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
    }
}
