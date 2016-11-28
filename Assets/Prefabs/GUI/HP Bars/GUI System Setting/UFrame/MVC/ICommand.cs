public interface ICommand
{
	object Data { get; set; }
	EventDispatcher Dispatcher { get; set; }
	void Init();
	void Execute(IEvent evt);
}