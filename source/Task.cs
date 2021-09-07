using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace cards
{
    class Task
    {
        public string Title = null, Content = null;
        public TaskState State = TaskState.INACTIVE;
        public DateTime StartTime, Deadline, SatisfiedUntil;
        public TimeSpan TimeRemaining => Deadline - DateTime.Now;
        public bool TimedOut => TimeRemaining <= TimeSpan.Zero;
        public TimeSpan? PredictedDuration = null;
        public TimeSpan? FinalDuration = null;
        public virtual int Weight { get; set; }
        public Color Color = Color.Gray;
        public int TimesRepeated = 0, TimesToRepeat = -1;
        public List<string> tags = new List<string>();
    }
    class Supertask : Task
    {
        public List<Task> Subtasks = new List<Task>();
        private int? weight = null;
        public override int Weight {
            get
            {
                if(weight == null)
                {
                    int ct = 0;
                    foreach (Task t in Subtasks) ct += t.Weight;
                    return ct;
                }
                if(weight <= 0)
                {
                    return 1;
                }
                return (int)weight;
            }
        }
    }
    enum TaskState
    {
        INACTIVE,   // The Task is not active and is only visible in the task list
        ACTIVE,     // The Task is active and is displayed on the details screen
        COMPLETE,   // The Task is fully complete
        ABANDONED,  // The Task has been removed from the list of incomplete tasks without being completed
        SATISFIED   // The Task is repeating with a delay between repeats and does not need to be completed until the SatisfiedUntil date.
    }
}
