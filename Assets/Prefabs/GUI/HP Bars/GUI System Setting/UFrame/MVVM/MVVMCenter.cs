using System.Collections.Generic;

namespace MVVM
{
	public class MVVMCenter
	{
		private List<IView> _views;
		private List<IViewModel> _viewModels;

		public static MVVMCenter Instance = new MVVMCenter();

		public MVVMCenter()
		{
			_views = new List<IView>();
			_viewModels = new List<IViewModel>();
		}

		public bool Register(IView view)
		{
			if (view == null || _views.Contains(view))
			{
				return false;
			}
			_views.Add(view);
			return true;
		}

		public bool Register(IViewModel viewModel)
		{
			if (viewModel == null || _viewModels.Contains(viewModel))
			{
				return false;
			}
			_viewModels.Add(viewModel);
			return true;
		}

		public void UnRegister(IView view)
		{
			if (view != null && _views.Contains(view))
			{
				_views.Remove(view);
			}
		}

		public void UnRegister(IViewModel viewModel)
		{
			if (viewModel != null && _viewModels.Contains(viewModel))
			{
				_viewModels.Remove(viewModel);
			}
		}
	}
}