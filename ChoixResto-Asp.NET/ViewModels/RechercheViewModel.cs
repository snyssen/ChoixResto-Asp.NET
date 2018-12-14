using ChoixResto_Asp.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChoixResto_Asp.NET.ViewModels
{
    public class RechercheViewModel
    {
        public string Recherche { get; set; }
        public List<Resto> ListeDesRestos { get; set; }
    }
}