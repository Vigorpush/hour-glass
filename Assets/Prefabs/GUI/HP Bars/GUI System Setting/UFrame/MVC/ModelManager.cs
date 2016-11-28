using System.Collections.Generic;
using UnityEngine;

public interface IModelManager
{
	void AddModel(IModel model);
	bool HasModel(string name);
	IModel GetModel(string name);
	void RemoveModel(string name);
}

public class ModelManager : IModelManager
{
	private readonly Dictionary<string, IModel> _models;
	private EventDispatcher _dispatcher;

	public ModelManager(EventDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
		_models = new Dictionary<string, IModel>();
	}

	public void AddModel(IModel model)
	{
		if (model == null)
		{
			Debug.LogError(string.Format("Error in {0} Model can't be null.", this));
			return;
		}
		if (HasModel(model.Name))
		{
			Debug.LogError(string.Format("Error in {0} Model '{1}' already registered.", this, model.Name));
			return;
		}

		lock (_models)
		{
			_models[model.Name] = model;
			model.Dispatcher = _dispatcher;
			model.Init();
		}
	}

	public bool HasModel(string name)
	{
		return _models.ContainsKey(name);
	}

	public IModel GetModel(string name)
	{
		return HasModel(name) ? _models[name] : null;
	}

	public void RemoveModel(string name)
	{
		if (!HasModel(name))
		{
			Debug.LogError(string.Format("Error in {0} Model '{1}' not registered.", this, name));
			return;
		}

		lock (_models)
		{
			_models[name].Dispose();
			_models.Remove(name);
		}
	}

	public void Dispose()
	{
		foreach (var model in _models)
		{
			model.Value.Dispose();
		}

		_models.Clear();
		_dispatcher = null;
	}
}