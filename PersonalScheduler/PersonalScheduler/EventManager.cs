﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace PersonalScheduler
{
    public class EventManager
    {
        public event Action<ScheduledEvent> OnEventAdded;
        public event Action<ScheduledEvent> OnEventRemoved;
        List<ScheduledEvent> listofevents = new List<ScheduledEvent>();
        Notifiers.SoundNotifier Sound = new Notifiers.SoundNotifier();
        Notifiers.VisualNotifier Visual = new Notifiers.VisualNotifier();
        Notifiers.EmailNotifier Email;
        private List<ScheduledEvent> Listofevents { get { return listofevents; } set { listofevents = value; listofevents.Sort((x, y) => x.DateTime.CompareTo(y.DateTime)); } }


        public void ProcessEvents()
        {
            while ((listofevents.Count != 0) && (listofevents[0].DateTime <= DateTime.Now))
            {
                foreach (var Notification in listofevents[0].Notifications)
                {
                    if ((Notification == NotificationType.Email) && (Email!=null))
                    {
                        Email.Notify(listofevents[0]);
                    }
                    if (Notification == NotificationType.Sound)
                    {
                        Sound.Notify(listofevents[0]);
                    }
                    if (Notification == NotificationType.Visual)
                    {
                        Visual.Notify(listofevents[0]);
                    }
                }
                if (listofevents[0] is RegularEvent)
                {
                    var ev = listofevents[0] as RegularEvent;
                    RemoveEvent(listofevents[0]);
                    AddEvent(new ScheduledEvent(ev.Name, ev.DateTime + ev.RepeatInterval, ev.Place, ev.Description, ev.Notifications));
                }
                else
                {
                    RemoveEvent(listofevents[0]);
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
                        Email = new Notifiers.EmailNotifier();
                    }
                    catch
                    {
                        MessageBoxResult result = System.Windows.MessageBox.Show(String.Join(Environment.NewLine, "Retry?", 
                                                                                             "Press No if you don't need Email Noptification", 
                                                                                             "Authentication error", MessageBoxButton.YesNo, 
                                                                                             MessageBoxImage.Error));
                        if (result == MessageBoxResult.Yes)
                        {
                            Email = null;
                            if (Directory.Exists(".credentials")) Directory.Delete(".credentials", true);
                            Email = new Notifiers.EmailNotifier();
                        };
                    }

                }
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
