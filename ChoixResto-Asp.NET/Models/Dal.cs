using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ChoixResto_Asp.NET.Models
{
	public class Dal : IDal
	{
		private BddContext bdd;

		public Dal()
		{
			bdd = new BddContext();
		}

		public List<Resto> ObtientTousLesRestaurants()
		{
			return bdd.Restos.ToList();
		}

		public void Dispose()
		{
			bdd.Dispose();
		}

		public void CreerRestaurant(string nom, string telephone)
		{
			bdd.Restos.Add(new Resto { Nom = nom, Telephone = telephone });
			bdd.SaveChanges();
		}

		public void ModifierRestaurant(int id, string nom, string telephone)
		{
			Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Id == id);
			if (restoTrouve != null)
			{
				restoTrouve.Nom = nom;
				restoTrouve.Telephone = telephone;
				bdd.SaveChanges();
			}
		}

		public bool RestaurantExiste(string nom)
		{
			Resto restoTrouve = bdd.Restos.FirstOrDefault(resto => resto.Nom == nom);
			if (restoTrouve != null)
				return true;
			else
				return false;
		}



		public Utilisateur ObtenirUtilisateur(int ID)
		{
			return bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == ID);
		}

		public Utilisateur ObtenirUtilisateur(string IDstr)
		{
			if (int.TryParse(IDstr, out int ID))
				return bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == ID);
			else
				return null;
		}

		public int AjouterUtilisateur(string prenom, string motDePasse)
		{
			Utilisateur utilisateurCree = bdd.Utilisateurs.Add(new Utilisateur { Prenom = prenom, MotDePasse = EncodeMD5(motDePasse) });
			bdd.SaveChanges();
			return utilisateurCree.Id;
		}

		public Utilisateur Authentifier(string prenom, string motDePasse)
		{
			string motDePasseHash = EncodeMD5(motDePasse);
			return bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Prenom == prenom && utilisateur.MotDePasse == motDePasseHash);
		}



		public int CreerUnSondage()
		{
			Sondage sondageCree = bdd.Sondages.Add(new Sondage { Date = DateTime.Now });
			bdd.SaveChanges();
			return sondageCree.Id;
		}

		public bool ADejaVote(int IDSondage, int IDUtilisateur)
		{
			Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == IDSondage);
			foreach (Vote vote in sondageTrouve.Votes)
			{
				if (vote.Utilisateur.Id == IDUtilisateur)
					return true;
			}
			return false;
		}

		public bool ADejaVote(int IDSondage, string IDUtilisateurstr)
		{
			if (!int.TryParse(IDUtilisateurstr, out int IDUtilisateur))
				return false;
			else
			{
				Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == IDSondage);
				if (sondageTrouve.Votes == null)
					return false;
				foreach (Vote vote in sondageTrouve.Votes)
				{
					if (vote.Utilisateur.Id == IDUtilisateur)
						return true;
				}
				return false;
			}
		}

		public void AjouterVote(int IDSondage, int IDResto, int IDUtilisateur)
		{
			Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == IDSondage);
			if (sondageTrouve.Votes == null) // Vérifie si il y a déjà une liste de votes d'initialisée, sinon la créée
				sondageTrouve.Votes = new List<Vote>();
			sondageTrouve.Votes.Add(new Vote { Resto = bdd.Restos.FirstOrDefault(resto => resto.Id == IDResto), Utilisateur = bdd.Utilisateurs.FirstOrDefault(utilisateur => utilisateur.Id == IDUtilisateur) });
			bdd.SaveChanges();
			//Vote voteCree = bdd.Votes.Add(new Vote { })
		}

		public List<Resultats> ObtenirLesResultats(int IDSondage)
		{
			List<Resultats> resultats = new List<Resultats>();
			Sondage sondageTrouve = bdd.Sondages.FirstOrDefault(sondage => sondage.Id == IDSondage);
			if (sondageTrouve == null)
				return null;
			foreach (Vote vote in sondageTrouve.Votes) // On parcourt les votes
			{
				bool restoDansResult = false;
				foreach (Resultats resultat in resultats) // on parcourt les résultats pour déterminer si le vote actuel contient ou non un restaurant déjà présent dans les résultats
				{
					if (resultat.Nom == vote.Resto.Nom) // Le resto est déjà dans les résultats => on veut ajouter un vote à ce resto
					{
						resultat.NombreDeVotes++;
						restoDansResult = true;
						break;
					}
				}
				if (!restoDansResult)
					resultats.Add(new Resultats { Nom = vote.Resto.Nom, Telephone = vote.Resto.Telephone, NombreDeVotes = 1 });
			}
			return resultats;
		}



		private string EncodeMD5(string motDePasse)
		{
			string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
			return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
		}
	}
}