using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChoixResto_Asp.NET.Models
{
	[Table("Restos")]
	public class Resto
	{
		public int Id { get; set; }
		[Required]
		public string Nom { get; set; }
		public string Telephone { get; set; }
	}
}