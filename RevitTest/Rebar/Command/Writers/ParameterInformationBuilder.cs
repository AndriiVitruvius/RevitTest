using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTest.Rebar.Command.Writers
{
	public class ParameterInformationBuilder
	{
		public StringBuilder stringBuilder { get; set; }
		public Element Element { get; }

		public ParameterInformationBuilder(Element element)
		{
			if (element == null)
				throw new ArgumentNullException(nameof(element));
			
			this.Element = element;
			stringBuilder = new StringBuilder();
		}
	}
}
