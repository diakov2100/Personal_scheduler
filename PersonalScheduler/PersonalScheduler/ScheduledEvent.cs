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
        protected string name;
        protected DateTime datetime;
        protected string description;
        protected string place;
        protected List<NotificationType> notifications;
        public string Name
        {
            get
            {
                return name;
            }

            protected set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name cannot be set as null.");
                }
                else if ((value == "") && (value.Count(c => c == ' ') == value.Length))
                {
                    throw new ArgumentException("Name cannot be set as an empty string or a string of whitespaces.");
                }
                else 
                {
                    name = value;
                }
            }
        }
        public DateTime DateTime
        {
            get
            {
                return datetime;
            }
            protected set
            {
                if (value == null)
                {
                    throw new ArgumentOutOfRangeException("DateTime cannot be set as null.");
                }
                else if (value <DateTime.Now)
                {
                    throw new ArgumentOutOfRangeException("DateTime cannot be earlier than the current moment.");
                }
                else
                {
                    datetime = value;
                }
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            protected set
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
            protected set
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
            protected set
            {
                if (value.Count < 1)
                {
                    throw new ArgumentException("Minimum one notification should be selected.");
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
