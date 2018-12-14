﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto_Asp.NET.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }
    }
}