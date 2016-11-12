using UnityEngine;

public abstract class MVCController : MonoBehaviour, IModelManager, IViewManager, IWireManager, IController
{
	private EventDispatcher _dispatcher;
	private WireManager _wireManager;
	public Controller Controller { get; private set; }
	public ViewManager ViewManager { get; private set; }
	public ModelManager ModelManager { get; private set; }

	public void AddModel(IModel model)
	{
		ModelManager.AddModel(model);
	}

	public bool HasModel(string name)
	{
		return ModelManager.HasModel(name);
	}

	public IModel GetModel(string name)
	{
		return ModelManager.GetModel(name);
	}

	public void RemoveModel(string name)
	{
		ModelManager.RemoveModel(name);
	}

	public void AddCommand<T>(string name) where T : ICommand
	{
		Controller.AddCommand<T>(name);
	}

	public void RemoveCommand<T>(string name) where T : ICommand
	{
		Controller.RemoveCommand<T>(name);
	}

	public bool HasCommand(IEvent evt)
	{
		return Controller.HasCommand(evt);
	}

	public bool HasCommand<T>(string name) where T : ICommand
	{
		return Controller.HasCommand<T>(name);
	}

	public void AddView(IView view)
	{
		ViewManager.AddView(view);
	}

	public bool HasView(string name)
	{
		return ViewManager.HasView(name);
	}

	public IView GetView(string name)
	{
		return ViewManager.GetView(name);
	}

	public void RemoveView(string name)
	{
		ViewManager.RemoveView(name);
	}

	public void AddWire(IWire wire)
	{
		_wireManager.AddWire(wire);
	}

	public bool HasWire(string name)
	{
		return _wireManager.HasWire(name);
	}

	public IWire GetWire(string name)
	{
		return _wireManager.GetWire(name);
	}

	public void RemoveWire(string name)
	{
		_wireManager.RemoveWire(name);
	}

	public void Init()
	{
		_dispatcher = new EventDispatcher();
		_dispatcher.OnTriggerEvent += HandleOnTriggerEvent;

		Controller = new Controller(_dispatcher);
		ModelManager = new ModelManager(_dispatcher);
		ViewManager = new ViewManager(_dispatcher);
		_wireManager = new WireManager(_dispatcher);

		RegisterCommands();
		RegisterModels();
		RegisterViews();
		RegisterWires();
	}

	private void HandleOnTriggerEvent(IEvent evt)
	{
		Controller.ExecuteCommand(evt);
		ViewManager.NotifyViews(evt);
	}

	#region Abstract

	protected abstract void RegisterWires();

	protected abstract void RegisterViews();

	protected abstract void RegisterModels();

	protected abstract void RegisterCommands();

	#endregion
}