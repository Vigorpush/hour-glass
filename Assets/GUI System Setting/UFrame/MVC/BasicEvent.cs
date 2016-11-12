public class BasicEvent : IEvent
{
	public BasicEvent(string name, object data = null)
	{
		Name = name;
		Data = data;
	}

	public string Name { get; set; }
	public object Data { get; set; }
}