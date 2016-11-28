using System;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
	void AddCommand<T>(string name) where T : ICommand;
	void RemoveCommand<T>(string name) where T : ICommand;
	bool HasCommand(IEvent evt);
	bool HasCommand<T>(string name) where T : ICommand;
}

public class Controller : IController
{
	private readonly Dictionary<string, List<Type>> _commandList;
	private EventDispatcher _dispatcher;

	public Controller(EventDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
		_commandList = new Dictionary<string, List<Type>>();
	}

	public void AddCommand<T>(string name) where T : ICommand
	{
		lock (_commandList)
		{
			if (_commandList.ContainsKey(name))
			{
				_commandList[name].Add(typeof (T));
			}
			else
			{
				_commandList.Add(name, new List<Type> {typeof (T)});
			}
		}
	}

	public void RemoveCommand<T>(string name) where T : ICommand
	{
		if (!HasCommand<T>(name))
		{
			Debug.LogError(string.Format("Error in {0} Command '{1}' not registered.", this, name));
			return;
		}

		lock (_commandList)
		{
			_commandList[name].Remove(typeof (T));
		}
	}

	public bool HasCommand(IEvent evt)
	{
		return _commandList.ContainsKey(evt.Name);
	}

	public bool HasCommand<T>(string name) where T : ICommand
	{
		return _commandList.ContainsKey(name) && _commandList[name].Contains(typeof (T));
	}

	public void ExecuteCommand(IEvent evt)
	{
		if (HasCommand(evt))
		{
			foreach (Type type in _commandList[evt.Name])
			{
				var command = Activator.CreateInstance(type) as ICommand;
				command.Dispatcher = _dispatcher;
				command.Execute(evt);
			}
		}
	}

	public void Dispose()
	{
		foreach (var types in _commandList.Values)
		{
			types.Clear();
		}

		_commandList.Clear();
		_dispatcher = null;
	}
}