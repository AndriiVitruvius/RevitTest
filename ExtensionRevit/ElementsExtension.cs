using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionRevit
{
    public static class ElementsExtension
    {

		public enum GetParameters
		{
			Element,
			TypeElement,
		}
		public static IEnumerable<Element> GetElementsCategory(this Document doc, BuiltInCategory builtInCats)
		{
			IList<Element> elements;
			FilteredElementCollector finalCollector = new FilteredElementCollector(doc);
			finalCollector.OfCategory(builtInCats).WhereElementIsNotElementType();
			elements = finalCollector.ToElements();
			return elements;
		}


	

		public static void SetInParameter(this Element element, string nameParameters, string textInparametr)
		{

			var parametrElements = element.GetParameters(nameParameters);

			bool setInElem = false;

			foreach (var parametrElement in parametrElements)
			{
				StorageType storageTypeParameter = parametrElement.StorageType;

				if (parametrElement != null && !parametrElement.IsReadOnly && storageTypeParameter == StorageType.String)
				{
					parametrElement.Set(textInparametr);
					setInElem = true;
				}
			}

			if (!setInElem)
			{
				TaskDialog.Show("Error", "Don't set value in " + nameParameters);
				throw new Exception("Don't set value in " + nameParameters);
			}

		}


		public static ElementType GetElementsType(this Element element)
		{
			if (element == null)  throw new NullReferenceException();

			Document document= element.Document;

			return document.GetElement(element.GetTypeId()) as ElementType ;
		}

		public static T GetInfoParameter<T>(this Element element, string nameParameter, GetParameters getParameters)
		{
			Document document = element.Document;
			T Result;
			string nameElement = element.Name;
			StorageType storageType;

			//  Cheack type return

			if (typeof(T) == typeof(double))      storageType = StorageType.Double;
			
			else if (typeof(T) == typeof(string)) storageType = StorageType.String;
			
			else								  storageType = StorageType.None;



			// Cheack element or type

			IList<Parameter> parameters;

			if (getParameters == GetParameters.Element)
				parameters = element.GetParameters(nameParameter);
			else
				parameters = document.GetElement(element.GetTypeId()).GetParameters(nameParameter);


			if (parameters.Count() == 0)
			{
				TaskDialog.Show("Error", "Don't have in " + nameElement + " " + nameParameter);

				throw new Exception("Don't have " + nameParameter);
			}
			else 
			{
				Parameter parameter = element.LookupParameter(nameParameter);
				StorageType storageTypeParam = parameter.StorageType;
				if (storageTypeParam == storageType)
				{
					if (storageType == StorageType.Double)
						Result = (T)Convert.ChangeType(parameter.AsDouble(), typeof(T));
					else if (storageType == StorageType.String)
						Result = (T)Convert.ChangeType(parameter.AsString(), typeof(T));
					else
						throw new Exception("Wrong return type");
				}
				else
				{
					TaskDialog.Show("Error", nameParameter + " Parametr Storage Type not Number");
					throw new Exception(nameParameter + " Parametr Storage Type not Number ");
				}

			}


			return Result;
		}

		public static double FromFutToMM(this double number) =>
		   UnitUtils.ConvertFromInternalUnits(number, DisplayUnitType.DUT_METERS) * 1000;


	}
}
