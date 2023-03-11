using Autodesk.Revit.DB;
using ExtensionRevit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExtensionRevit.ElementsExtension;

namespace RevitTest.Rebar.Command.Writers
{
	internal class CommentWriterRebar : CommentsWriter
	{

		protected override string FamilyName => "Rebar";

		protected override void AddDiameter(ParameterInformationBuilder parameterInformation)
		{
			double Diameter = parameterInformation.Element.GetInfoParameter<double>("Rebar Diameter", GetParameters.Element);

			parameterInformation.stringBuilder.Append("Ø");
			parameterInformation.stringBuilder.Append(Math.Round(Diameter.FromFutToMM()));

		}

		protected override void AddGrade(ParameterInformationBuilder parameterInformation) =>
			parameterInformation.stringBuilder.Append(", Grade 60, ");
		

		protected override void AddLength(ParameterInformationBuilder parameterInformation)
		{
			double length = parameterInformation.Element.GetInfoParameter<double>("L", GetParameters.Element);

			parameterInformation.stringBuilder.Append("L=");
			parameterInformation.stringBuilder.Append(Math.Round(length.FromFutToMM()));

		}
	}
}
