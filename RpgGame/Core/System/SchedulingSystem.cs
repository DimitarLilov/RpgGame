using System.Collections.Generic;
using System.Linq;
using RpgGame.Interfaces;
using RpgGame.ModelDTOs.Interfaces;

namespace RpgGame.Core.System
{
    public class SchedulingSystem
    {
        private readonly SortedDictionary<int, List<IScheduleable>> scheduleables;
        private int time;

        public SchedulingSystem()
        {
            this.time = 0;
            this.scheduleables = new SortedDictionary<int, List<IScheduleable>>();
        }

        public void Add(IScheduleable scheduleable)
        {
            int key = this.time + scheduleable.Time;
            if (!this.scheduleables.ContainsKey(key))
            {
                this.scheduleables.Add(key, new List<IScheduleable>());
            }

            this.scheduleables[key].Add(scheduleable);
        }

        public void Remove(IScheduleable scheduleable)
        {
            KeyValuePair<int, List<IScheduleable>> scheduleableListFound
              = new KeyValuePair<int, List<IScheduleable>>(-1, null);

            foreach (var scheduleablesList in this.scheduleables)
            {
                if (scheduleablesList.Value.Contains(scheduleable))
                {
                    scheduleableListFound = scheduleablesList;
                    break;
                }
            }

            if (scheduleableListFound.Value != null)
            {
                scheduleableListFound.Value.Remove(scheduleable);
                if (scheduleableListFound.Value.Count <= 0)
                {
                    this.scheduleables.Remove(scheduleableListFound.Key);
                }
            }
        }

        public IScheduleable Get()
        {
            var firstScheduleableGroup = this.scheduleables.First();
            var firstScheduleable = firstScheduleableGroup.Value.First();
            this.Remove(firstScheduleable);
            this.time = firstScheduleableGroup.Key;
            return firstScheduleable;
        }

        public int GetTime()
        {
            return this.time;
        }

        public void Clear()
        {
            this.time = 0;
            this.scheduleables.Clear();
        }
    }
}