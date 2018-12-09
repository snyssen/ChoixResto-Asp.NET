using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto_Asp.NET.Models;

namespace ChoixResto_Asp.NET.Controllers
{
	public class AccueilController : Controller
	{
		private IDal dal;

		public AccueilController() : this(new Dal())
		{
		}

		public AccueilController(IDal dalIoc)
		{
			dal = dalIoc;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Index")]
		public ActionResult IndexPost()
		{
			int idSondage = dal.CreerUnSondage();
			return RedirectToAction("Index", "Vote", new { id = idSondage });
		}
	}
}