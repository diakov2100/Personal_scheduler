using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalScheduler.Notifiers
{
    interface INotifier
    {
        void Notify(ScheduledEvent e);
    }
}
