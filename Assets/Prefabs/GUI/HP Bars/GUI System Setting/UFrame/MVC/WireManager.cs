using System.Collections.Generic;
using UnityEngine;

public interface IWireManager
{
	void AddWire(IWire wire);
	bool HasWire(string name);
	IWire GetWire(string name);
	void RemoveWire(string name);
}

public class WireManager : IWireManager
{
	private readonly Dictionary<string, IWire> _wires;
	private EventDispatcher _dispatcher;

	public WireManager(EventDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
		_wires = new Dictionary<string, IWire>();
	}

	public void AddWire(IWire wire)
	{
		if (wire == null)
		{
			Debug.LogError(string.Format("Error in {0} Wire can't be null.", this));
			return;
		}

		if (HasWire(wire.Name))
		{
			Debug.LogError(string.Format("Error in {0} Wire '{1}' already registered.", this, wire.Name));
			return;
		}

		lock (_wires)
		{
			_wires.Add(wire.Name, wire);
			wire.Dispatcher = _dispatcher;
			wire.Init();
		}
	}

	public bool HasWire(string name)
	{
		return _wires.ContainsKey(name);
	}

	public IWire GetWire(string name)
	{
		return HasWire(name) ? _wires[name] : null;
	}

	public void RemoveWire(string name)
	{
		if (!HasWire(name))
		{
			Debug.LogError(string.Format("Error in {0} Wire '{1}' not registered.", this, name));
			return;
		}

		lock (_wires)
		{
			_wires[name].Dispose();
			_wires.Remove(name);
		}
	}

	public void Dispose()
	{
		foreach (var wire in _wires)
		{
			wire.Value.Dispose();
		}

		_wires.Clear();
		_dispatcher = null;
	}
}