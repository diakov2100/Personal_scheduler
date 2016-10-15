using System.Windows;

namespace PersonalScheduler.Notifiers
{
	class VisualNotifier : INotifier
    {
		public void Notify(ScheduledEvent ev)
		{
			MessageBox.Show("Sample message");
		}
	}
}
