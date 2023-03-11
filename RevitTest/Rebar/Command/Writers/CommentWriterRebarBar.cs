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
	internal class CommentWriterRebarBar : CommentsWriter
	{
		protected override string FamilyName => "Rebar Bar";

		protected override void AddDiameter(ParameterInformationBuilder parameterInformation)
		{
			double Diameter = parameterInformation.Element.GetInfoParameter<double>("Bar Diameter", GetParameters.TypeElement);

			parameterInformation.stringBuilder.Append("Ø");
			parameterInformation.stringBuilder.Append(Math.Round(Diameter.FromFutToMM()));
		}


		protected override void AddGrade(ParameterInformationBuilder parameterInformation) =>
			parameterInformation.stringBuilder.Append(", Grade 60, ");
		



		protected override void AddLength(ParameterInformationBuilder parameterInformation)
		{
			double length = parameterInformation.Element.GetInfoParameter<double>("Total Bar Length", GetParameters.Element);

			parameterInformation.stringBuilder.Append("L=");
			parameterInformation.stringBuilder.Append(Math.Round(length.FromFutToMM()));
		}

	}
}
