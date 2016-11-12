public interface IModel
{
	string Name { get; set; }
	object Data { get; set; }
	EventDispatcher Dispatcher { get; set; }
	void Init();
	void Dispose();
}