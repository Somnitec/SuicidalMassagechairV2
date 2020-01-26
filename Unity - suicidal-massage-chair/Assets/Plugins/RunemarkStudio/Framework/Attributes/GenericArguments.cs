namespace Runemark.VisualEditor
{
    using System;

    [AttributeUsage(AttributeTargets.Class, AllowMultiple=false)]
	public class GenericArguments : Attribute
	{
		public readonly Type[] Types;
		public GenericArguments(params Type[] args)
		{
			Types = args;
		}
	}
}