using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoixResto_Asp.NET.Models
{
	public interface IDal : IDisposable
	{
		void CreerRestaurant(string nom, string telephone);
		void ModifierRestaurant(int id, string nom, string telephone);
		bool RestaurantExiste(string nom);
		List<Resto> ObtientTousLesRestaurants();

		Utilisateur ObtenirUtilisateur(int ID);
		Utilisateur ObtenirUtilisateur(string IDstr);
		int AjouterUtilisateur(string prenom, string motDePasse);
		Utilisateur Authentifier(string prenom, string motDePasse);

		int CreerUnSondage();
		bool ADejaVote(int IDSondage, int IDUtilisateur);
		bool ADejaVote(int IDSondage, string IDUtilisateurstr);
		void AjouterVote(int IDSondage, int IDResto, int IDUtilisateur);
		List<Resultats> ObtenirLesResultats(int IDSondage);
	}
}
