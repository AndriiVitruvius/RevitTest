using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionRevit
{
	public static class Extention
	{
		public static void CallWithTransaction(this Document doc ,Action action)
		{
			// Get the TransactionNameAttribute from the method
			TransactionNameAttribute attribute = (TransactionNameAttribute)Attribute.GetCustomAttribute(action.Method, typeof(TransactionNameAttribute));

			string transactionName = attribute?.TransactionName ?? "Transaction";


			using (Transaction tx = new Transaction(doc))
			{
				try
				{
					tx.Start(transactionName);
					action();
					tx.Commit();
				}
				catch (Exception ex)
				{
					tx.RollBack();
					throw new Exception(ex.ToString(),ex);
				}
			}
		}


		[AttributeUsage(AttributeTargets.Method)]
		public class TransactionNameAttribute : Attribute
		{
			public string TransactionName { get; set; }

			public TransactionNameAttribute(string transactionName)
			{
				TransactionName = transactionName;
			}
		}

		
	}
}
