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
using static ExtensionRevit.Extention;

namespace RevitTest.ExternalCommands
{
	/// <summary>
	/// Add in commnets information Diameter Grade Length  to Rebars 
	/// </summary>
	/// 


	[Transaction(TransactionMode.Manual)]

	public class AddCommentsExternalCommand : ExternalCommand
	{

		public override void Execute()
		{

			Writer[] writers = new Writer[] 
			{
				new CommentWriterRebar(),
				new CommentWriterRebarBar() 
			};

		
			IEnumerable<Autodesk.Revit.DB.Element> Rebars = doc.GetElementsCategory(Autodesk.Revit.DB.BuiltInCategory.OST_Rebar);
			doc.CallWithTransaction(() => AddNewComment(writers, Rebars));

			TaskDialog.Show("Information", "Successfully");

		}

		[TransactionName("Add Comments for Rebar")]
		private static void AddNewComment(Writer[] writers, IEnumerable<Element> Rebars)
		{
			foreach (var item in Rebars)
				foreach (var writer in writers)
					if (writer.CheackTypeWriterElement(item))
					{
						writer.SetInformation(new ParameterInformationBuilder(item));
						break;
					}

			
		}
	}
}
