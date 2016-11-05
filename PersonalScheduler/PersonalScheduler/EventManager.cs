using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PersonalScheduler.Notifiers;


namespace PersonalScheduler
{
    public class EventManager
    {
        public event Action<ScheduledEvent> OnEventAdded;
        public event Action<ScheduledEvent> OnEventRemoved;
        List<ScheduledEvent> listofevents = new List<ScheduledEvent>();
        INotifier Sound = NotiFactory.Default.GetSound();
        INotifier Visual = NotiFactory.Default.GetVisual();
        INotifier Email;

        public void ProcessEvents()
        {
            while ((listofevents.Count != 0) && (listofevents[0].DateTime <= DateTime.Now))
            {
                var tecev = listofevents[0];
                if (listofevents[0] is RegularEvent)
                {
                    var ev = listofevents[0] as RegularEvent;

                    RemoveEvent(listofevents[0]);
                    AddEvent(new RegularEvent(ev.RepeatInterval, ev.Name, ev.DateTime + ev.RepeatInterval, ev.Place, ev.Description, ev.Notifications));
                }
                else
                {
                    RemoveEvent(listofevents[0]);
                }
                foreach (var Notification in tecev.Notifications)
                {
                    if ((Notification == NotificationType.Email) && (Email != null))
                    {
                        Email.Notify(tecev);
                    }
                    if (Notification == NotificationType.Sound)
                    {
                        Sound.Notify(tecev);
                    }
                    if (Notification == NotificationType.Visual)
                    {
                        Visual.Notify(tecev);
                    }
                }

            }

        }

        public void AddEvent(ScheduledEvent ev)
        {
            if (!listofevents.Any(item => item.Name == ev.Name))
            {
                if ((Email == null) && (ev.Notifications.Contains(NotificationType.Email)))
                {

                    try
                    {
                        Email = NotiFactory.Default.GetEmail();
                    }
                    catch
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show(String.Join(Environment.NewLine, "Retry?",
                                                                                             "Press No if you don't need Email Noptification"), 
                                                                                             "Authentication error", MessageBoxButton.YesNo,
                                                                                             MessageBoxImage.Error);
                        if (result == MessageBoxResult.Yes)
                        {
                            Email = null;
                            if (Directory.Exists(".credentials")) Directory.Delete(".credentials", true);
                            Email = NotiFactory.Default.GetEmail();
                        };
                    }

                }
                int index = listofevents.FindIndex(e => e.DateTime > ev.DateTime);
                if (index>=0)
                    listofevents.Insert(index, ev);
                else
                    listofevents.Add(ev);
                OnEventAdded?.Invoke(ev);

            }
            else
            {
                throw new ArgumentException("The event witn such method had been already added");
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
