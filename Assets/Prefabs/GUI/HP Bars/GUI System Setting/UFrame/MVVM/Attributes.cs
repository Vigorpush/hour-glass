using System;

namespace MVVM
{
	[AttributeUsage(AttributeTargets.Class)]
	public class ViewAttribute : Attribute
	{
		public Type ContextType { get; set; }
	}

	[AttributeUsage(AttributeTargets.Class)]
	public class BindingClassAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Interface)]
	public class BindingInterfaceAttribute : Attribute
	{
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class BindingPropertyAttribute : Attribute
	{
		public string BindingKey { get; set; }

		public BindingPropertyAttribute(string bindingKey)
		{
			BindingKey = bindingKey;
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class BindingMethodAttribute : Attribute
	{
		public string BindingKey { get; set; }

		public BindingMethodAttribute(string bindingKey)
		{
			BindingKey = bindingKey;
		}
	}

	[AttributeUsage(AttributeTargets.Property)]
	public class ViewBindingPropertyAttribute : Attribute
	{
		public string TargetKey { get; set; }

		public ViewBindingPropertyAttribute(string targetKey)
		{
			TargetKey = targetKey;
		}
	}

	[AttributeUsage(AttributeTargets.Method)]
	public class ViewBindingMethodAttribute : Attribute
	{
		public string TargetKey { get; set; }

		public ViewBindingMethodAttribute(string targetKey)
		{
			TargetKey = targetKey;
		}
	}
}