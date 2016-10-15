using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalScheduler
{
    public class EventManager
    {
        public event Action<ScheduledEvent> OnEventAdded;
        public event Action<ScheduledEvent> OnEventRemoved;
        List<ScheduledEvent> listofevents = new List<ScheduledEvent>();
        private List<ScheduledEvent> Listofevents { get { return listofevents; } set { listofevents = value; listofevents.Sort((x, y) => x.DateTime.CompareTo(y.DateTime)); } }

        public void ProcessEvents()
        {

        }

        public void AddEvent(ScheduledEvent ev)
        {
            if (!listofevents.Any(item => item.Name == ev.Name))
            {
                Listofevents.Add(ev);
                OnEventAdded?.Invoke(ev);
            }
            else
            {
                throw new ArgumentException("");
            }


        }

        public void RemoveEvent(ScheduledEvent ev)
        {
            if (listofevents.Contains(ev))
            {
                listofevents.Remove(ev);
                OnEventRemoved?.Invoke(ev);
            }
        }
    }
}
