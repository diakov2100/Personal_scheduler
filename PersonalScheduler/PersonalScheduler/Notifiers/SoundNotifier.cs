namespace PersonalScheduler.Notifiers
{
	class SoundNotifier : INotifier
    {
		public void Notify(ScheduledEvent e) 
		{
			System.Media.SystemSounds.Exclamation.Play();
		}
	}
}
