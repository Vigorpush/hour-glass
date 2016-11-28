public class EventDispatcher
{
	public event TriggerEvent OnTriggerEvent;

	public void TriggerEvent(IEvent evt)
	{
		if (OnTriggerEvent != null)
		{
			OnTriggerEvent(evt);
		}
	}
}