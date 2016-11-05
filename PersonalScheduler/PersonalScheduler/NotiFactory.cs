using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalScheduler.Notifiers;

namespace PersonalScheduler
{
    class NotiFactory
    {
        static NotiFactory _default;

        public static NotiFactory Default
        {
            get
            {
                return _default = new NotiFactory();
            }
        }

        private INotifier _notifyers;

        public INotifier GetVisual()
        {
            _notifyers = (new VisualNotifier());
            return _notifyers;
        }

        public INotifier GetSound()
        {
            _notifyers = (new SoundNotifier());
            return _notifyers;
        }

        public INotifier GetEmail()
        {
            _notifyers = (new EmailNotifier());
            return _notifyers;
        }
    }
}
