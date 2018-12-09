using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChoixResto_Asp.NET.Controllers;
using System.Web.Mvc;

namespace ChoixResto_Asp.NET.Tests.Controllers
{
	[TestClass]
	public class AccueilControllerTest
	{
		[TestMethod]
		public void AccueilController_Index_RenvoiVueParDefaut()
		{
			AccueilController controller = new AccueilController();

			ViewResult resultat = (ViewResult)controller.Index();

			Assert.AreEqual(string.Empty, resultat.ViewName);
		}
	}
}
