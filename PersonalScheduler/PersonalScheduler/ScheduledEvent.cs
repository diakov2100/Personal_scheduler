using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonalScheduler
{
    public enum NotificationType
    {
        Email,
        Sound,
        Visual
    }

    public class ScheduledEvent
    {
        private string name;
        private DateTime datetime;
        private string description;
        private string place;
        private List<NotificationType> notifications;
        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                if ((value != null) && (value != "") && (value.Count(c => c == ' ') != value.Length))
                {
                    name = value;
                }
                else if ((value == "") && (value.Count(c => c == ' ') == value.Length))
                {
                    throw new ArgumentException();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
        }
        public DateTime DateTime
        {
            get
            {
                return datetime;
            }
            private set
            {
                if ((value != null))
                {
                    datetime = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("");
                }
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            private set
            {
                description = value;
            }
        }
        public string Place
        {
            get
            {
                return place;
            }
            private set
            {
                place = value;
            }
        }
        public List<NotificationType> Notifications
        {
            get
            {
                return notifications;
            }
            private set
            {
                if (value.Count < 1)
                {
                    throw new ArgumentException();
                }
                else
                {
                    notifications = value;
                }
            }
        }
        public ScheduledEvent(string Name, DateTime DateTime, string Description, string Place, List<NotificationType> Notifications)
        {
            this.Name = Name;
            this.DateTime = DateTime;
            this.Description = Description;
            this.Place = Place;
            this.Notifications = Notifications;
        }
    }
}
