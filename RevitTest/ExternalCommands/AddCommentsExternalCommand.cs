using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ExtensionRevit;
using RevitTest.Rebar.Command.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ExtensionRevit.TransactionExtension;

namespace RevitTest.ExternalCommands
{
	/// <summary>
	/// Add in commnets information Diameter Grade Length  to Rebars 
	/// </summary>
	/// 


	[Transaction(TransactionMode.Manual)]

	public class AddCommentsExternalCommand : ExternalCommand
	{
		IEnumerable<Autodesk.Revit.DB.Element> Rebars;

		Writer[] Writers = new Writer[]
	    {
				new CommentWriterRebar(),
				new CommentWriterRebarBar()
	    };

		public override void Execute()
		{

		   Rebars = doc.GetElementsCategory(Autodesk.Revit.DB.BuiltInCategory.OST_Rebar);

		   doc.CallWithTransaction(AddNewComment);

			TaskDialog.Show("Information", "Successfully");

		}

		[TransactionName("Add Comments for Rebar")]
		private void AddNewComment()
		{
			foreach (var item in Rebars)
				foreach (var writer in Writers)
					if (writer.CheackTypeWriterElement(item))
					{
						writer.SetInformation(new ParameterInformationBuilder(item));
						break;
					}

			
		}
	}
}
