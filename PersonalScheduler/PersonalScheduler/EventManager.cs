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
        Notifiers.SoundNotifier Sound = new Notifiers.SoundNotifier();
        Notifiers.VisualNotifier Visual = new Notifiers.VisualNotifier();
        private List<ScheduledEvent> Listofevents { get { return listofevents; } set { listofevents = value; listofevents.Sort((x, y) => x.DateTime.CompareTo(y.DateTime)); } }

        public void ProcessEvents()
        {
            if (listofevents.Count != 0)
            {
                int i = 0;
                while ((i<listofevents.Count) && (listofevents[i].DateTime<=DateTime.Now))
                { 
                    foreach (var Notification in listofevents[i].Notifications)
                    {
                        if (Notification == NotificationType.Email)
                        {

                        }
                        if (Notification == NotificationType.Sound)
                        {
                            Sound.Notify(listofevents[i]);
                        }
                        if (Notification == NotificationType.Visual)
                        {
                            Visual.Notify(listofevents[i]);
                        }
                    }
                    var ev = listofevents[0] as RegularEvent;
                    RemoveEvent(listofevents[i]);
                    i++;
                }
            }
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
