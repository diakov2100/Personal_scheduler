namespace PersonalScheduler.Notifiers
{
	class SoundNotifier : INotifier
    {
		public void Notify(ScheduledEvent ev) 
		{
			System.Media.SystemSounds.Exclamation.Play();
		}
	}
}
