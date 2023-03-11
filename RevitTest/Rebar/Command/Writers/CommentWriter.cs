using Autodesk.Revit.DB;
using ExtensionRevit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitTest.Rebar.Command.Writers
{
	/// <summary>
	/// Make string with diameter , Grade , length 
	/// </summary>
	internal abstract class CommentsWriter : Writer
	{
		protected override string ParameterName => "Comments";


		protected override ParameterInformationBuilder MakeInformation(ParameterInformationBuilder parameterInformation)
		{
			AddDiameter(parameterInformation);
			AddGrade(parameterInformation);
			AddLength(parameterInformation);

			return parameterInformation;
		}


		protected abstract void AddDiameter(ParameterInformationBuilder parameterInformation);
		protected abstract void AddLength(ParameterInformationBuilder parameterInformation);
		protected abstract void AddGrade(ParameterInformationBuilder parameterInformation);
	}
}
