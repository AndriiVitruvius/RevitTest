using Autodesk.Revit.UI;
using System.Drawing;
using System.Windows.Media.Imaging;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing.Imaging;
using System.IO;

namespace ExtensionRevit
{
	public static class RibbonExtension
	{

		public static RibbonPanel CreatePanel(this UIControlledApplication application, string panelName, string tabName)
		{

			RibbonPanel ribbonPanel = null;
			foreach (RibbonPanel ribbonPanel2 in application.GetRibbonPanels(tabName))
			{
				if (ribbonPanel2.Name.Equals(panelName))
				{
					ribbonPanel = ribbonPanel2;
					break;
				}
			}

			return ribbonPanel ?? application.CreateRibbonPanel(tabName, panelName);
		}


		public static void CreateTab(this UIControlledApplication application, string tabName)
		{
			try
			{
				application.CreateRibbonTab(tabName);
			}
			catch (Exception ex)
			{
				TaskDialog.Show("Error", ex.ToString());
			}



		}

		public static PushButton AddPushButton<TCommand>(this RibbonPanel panel, string buttonText) where TCommand : IExternalCommand, new()
		{
			Type typeFromHandle = typeof(TCommand);
			PushButtonData itemData = new PushButtonData(typeFromHandle.FullName, buttonText, Assembly.GetAssembly(typeFromHandle).Location, typeFromHandle.FullName);
			return (PushButton)panel.AddItem(itemData);
		}

		public static PushButton AddLongDescription(this PushButton pushButton, string Description)
		{
			pushButton.LongDescription = Description;	
			return pushButton;
		}

		public static PushButton AddToolTip(this PushButton pushButton, string Description)
		{
			pushButton.LongDescription = Description;
			return pushButton;
		}

		public static PushButton AddImageLarge(this PushButton pushButton,  System.Drawing.Image Image )
		{
			pushButton.LargeImage = GetImageSourc(Image);
			return pushButton;
		}

		private static BitmapImage GetImageSourc(System.Drawing.Image img)
		{
			BitmapImage bmp = new BitmapImage();

			using (MemoryStream ms = new MemoryStream())
			{
				img.Save(ms, ImageFormat.Png);
				ms.Position = 0;

				bmp.BeginInit();
				bmp.StreamSource = ms;
				bmp.CacheOption = BitmapCacheOption.OnLoad;
				bmp.UriSource = null;
				bmp.EndInit();


			}
			return bmp;
		}

	}
}
