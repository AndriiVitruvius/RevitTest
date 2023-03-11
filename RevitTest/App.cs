#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using ExtensionRevit;
using RevitTest.ExternalCommands;
using System;
using System.Collections.Generic;

#endregion

namespace RevitTest
{
	internal class App : IExternalApplication
	{
		const string nameTab = "Test Task";



		public Result OnStartup(UIControlledApplication a)
		{
			a.CreateTab(nameTab);


			RibbonPanel ribbonPanel = a.CreatePanel("Set Information" , nameTab);

			PushButton pushButtonAddComments = ribbonPanel.AddPushButton<AddCommentsExternalCommand>("Add Rebar's Information");

			pushButtonAddComments.AddToolTip("Button For Add  main Information to Comments")
					             .AddLongDescription("You can add Diameter Grid and Length to Comment for all rebar in Project")
							     .AddImageLarge(Properties.Resources.icon);
					
			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication a)
		{
			return Result.Succeeded;
		}
	}
}
