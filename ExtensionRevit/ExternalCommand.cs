using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionRevit
{

	public abstract class ExternalCommand : IExternalCommand
	{
		public UIApplication uiapp;
		public UIDocument uidoc ;
		public Document doc;

	

	    [EditorBrowsable(EditorBrowsableState.Never)]
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			 Debugger.Launch();


			 uiapp = commandData.Application;
			 uidoc = uiapp.ActiveUIDocument;
			 doc = uidoc.Document;

			try
			{
				Execute();
			}
			catch (Exception ex)
			{
				TaskDialog.Show("Error", ex.Message.ToString()); 
				return Result.Failed;
			}
			finally
			{
				//TODO: logger
			}

			return Result.Succeeded;
		}

		public abstract void Execute();


	}

}
