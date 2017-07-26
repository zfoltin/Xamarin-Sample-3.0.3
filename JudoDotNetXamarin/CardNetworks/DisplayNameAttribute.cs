using System;
using System.Reflection;

namespace JudoDotNetXamarin
{
	public class DisplayName : System.Attribute
	{ 
		public string Name { get; set; }

		public DisplayName(string name)
		{
			Name = name;
		}
	}
}
