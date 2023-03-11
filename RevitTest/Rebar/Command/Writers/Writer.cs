using Autodesk.Revit.DB;
using ExtensionRevit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTest.Rebar.Command.Writers
{
	public abstract class Writer
	{
		protected abstract string FamilyName { get;  } 
		/// <summary>
		/// Parameter for Seting
		/// </summary>
		protected abstract string ParameterName { get; }

		/// <summary>
		/// Cheack of FamilyName 		
		/// </summary>
		public bool CheackTypeWriterElement(Element element)
		{
			var  elementType = element.GetElementsType();
		    if (elementType.FamilyName == FamilyName)
			  	return true;

			return false; 
		}

		protected abstract ParameterInformationBuilder MakeInformation(ParameterInformationBuilder stringBuilder);

		internal ParameterInformationBuilder SetInformation(ParameterInformationBuilder parameterInformationBuilder)
		{
			MakeInformation(parameterInformationBuilder);
			parameterInformationBuilder.Element.SetInParameter(ParameterName, parameterInformationBuilder.stringBuilder.ToString());
			return parameterInformationBuilder;	
		}

	}
}
