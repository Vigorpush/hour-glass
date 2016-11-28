using System.Collections.Generic;
using UnityEngine;

public interface IViewManager
{
	void AddView(IView view);
	bool HasView(string name);
	IView GetView(string name);
	void RemoveView(string name);
}

public class ViewManager : IViewManager
{
	private readonly EventDispatcher _dispatcher;
	private readonly Dictionary<string, IView> _views;
	private readonly Dictionary<string, List<IView>> _viewsToNotify;

	public ViewManager(EventDispatcher dispatcher)
	{
		_dispatcher = dispatcher;
		_views = new Dictionary<string, IView>();
		_viewsToNotify = new Dictionary<string, List<IView>>();
	}

	public void AddView(IView view)
	{
		if (view == null)
		{
			Debug.LogError(string.Format("Error in {0} View can't be null.", this));
			return;
		}

		if (HasView(view.Name))
		{
			Debug.LogError(string.Format("Error in {0} View '{1}' already registered.", this, view.Name));
			return;
		}

		lock (_views)
		{
			_views.Add(view.Name, view);
			view.Dispatcher = _dispatcher;

			if (view.EventList != null)
			{
				foreach (string name in view.EventList)
				{
					if (_viewsToNotify.ContainsKey(name))
					{
						_viewsToNotify[name].Add(view);
					}
					else
					{
						_viewsToNotify.Add(name, new List<IView> {view});
					}
				}
			}

			view.Init();
		}
	}

	public bool HasView(string name)
	{
		return _views.ContainsKey(name);
	}

	public IView GetView(string name)
	{
		return HasView(name) ? _views[name] : null;
	}

	public void RemoveView(string name)
	{
		if (!HasView(name))
		{
			Debug.Log(string.Format("Error in {0} View '{1}' don't registered.", this, name));
			return;
		}

		lock (_views)
		{
			_views[name].Dispose();
			_views.Remove(name);
		}
	}

	public bool HasView(IEvent evt)
	{
		return _viewsToNotify.ContainsKey(evt.Name);
	}

	public void NotifyViews(IEvent evt)
	{
		if (!HasView(evt))
		{
			return;
		}

		foreach (IView view in _viewsToNotify[evt.Name])
		{
			view.Notify(evt);
		}
	}

	public void Dispose()
	{
		foreach (var view in _views)
		{
			view.Value.Dispose();
		}

		_views.Clear();
		_viewsToNotify.Clear();
	}
}