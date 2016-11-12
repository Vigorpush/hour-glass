public interface IWire
{
	string Name { get; }
	EventDispatcher Dispatcher { get; set; }
	void Init();
	void Dispose();
}