using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalScheduler
{
    class RegularEvent : ScheduledEvent
    {
        private TimeSpan repeatInterval;
        public TimeSpan RepeatInterval
        {
            get { return repeatInterval; }
            private set
            {
                if ((notifications.Contains(NotificationType.Email)) && (value.Minutes<5))
                {
                    throw new ArgumentException();
                }
                else
                {
                    repeatInterval = value;
                }
            }

        }

        public RegularEvent(TimeSpan RepeatInterval, string Name, DateTime DateTime, string Description, string Place, List<NotificationType> Notifications) : base(Name, DateTime, Description, Place, Notifications)
        {
            this.RepeatInterval = RepeatInterval;
        }
    }
}
