public interface IEvent
{
	string Name { get; set; }
	object Data { get; set; }
}