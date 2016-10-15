using System.Windows;

namespace PersonalScheduler.Notifiers
{
	class VisualNotifier : INotifier
    {
		public void Notify(ScheduledEvent ev)
		{
            VisualNotifierWindow notifierWindow = new VisualNotifierWindow(ev);
            notifierWindow.ShowDialog();

        }
	}
}
