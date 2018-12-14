using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChoixResto_Asp.NET.Models;
using ChoixResto_Asp.NET.ViewModels;

namespace ChoixResto_Asp.NET.Controllers
{
	public class RestaurantController : Controller
	{
		private IDal dal;

		public RestaurantController() : this(new Dal())
		{
		}

		public RestaurantController(IDal dalIoc)
		{
			dal = dalIoc;
		}

		public ActionResult Index()
		{
			List<Resto> listeDesRestaurants = dal.ObtientTousLesRestaurants();
			return View(listeDesRestaurants);
		}

        public JsonResult VerifNomResto(string Nom)
        {
            bool resultat = !dal.RestaurantExiste(Nom);
            return Json(resultat, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreerRestaurant()
		{
			return View();
		}

		[HttpPost]
		public ActionResult CreerRestaurant(Resto resto)
		{
			if (dal.RestaurantExiste(resto.Nom))
			{
				ModelState.AddModelError("Nom", "Ce nom de restaurant existe déjà");
				return View(resto);
			}
			if (!ModelState.IsValid)
				return View(resto);
			dal.CreerRestaurant(resto.Nom, resto.Telephone);
			return RedirectToAction("Index");
		}

		public ActionResult ModifierRestaurant(int? id)
		{
			if (id.HasValue)
			{
				Resto restaurant = dal.ObtientTousLesRestaurants().FirstOrDefault(r => r.Id == id.Value);
				if (restaurant == null)
					return View("Error");
				return View(restaurant);
			}
			else
				return HttpNotFound();
		}

		[HttpPost]
		public ActionResult ModifierRestaurant(Resto resto)
		{
			if (!ModelState.IsValid)
				return View(resto);
			dal.ModifierRestaurant(resto.Id, resto.Nom, resto.Telephone);
			return RedirectToAction("Index");
		}

        public ActionResult Recherche(RechercheViewModel rechercheViewModel)
        {
            return View(rechercheViewModel);
        }

        public ActionResult ResultatsRecherche(RechercheViewModel rechercheViewModel)
        {
            if (!string.IsNullOrWhiteSpace(rechercheViewModel.Recherche))
                rechercheViewModel.ListeDesRestos = dal.ObtientTousLesRestaurants().Where(r => r.Nom.IndexOf(rechercheViewModel.Recherche, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
            else
                rechercheViewModel.ListeDesRestos = new List<Resto>();
            return PartialView(rechercheViewModel);
        }
    }
}