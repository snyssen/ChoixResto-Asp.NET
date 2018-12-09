using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ChoixResto_Asp.NET.Models
{
	public class InitChoixResto : DropCreateDatabaseAlways<BddContext>
	{
		protected override void Seed(BddContext context)
		{
			context.Restos.Add(new Resto { Id = 1, Nom = "Resto pinambour", Telephone = "0492698811"});
			context.Restos.Add(new Resto { Id = 2, Nom = "Resto pinière", Telephone = "0492698811" });
			context.Restos.Add(new Resto { Id = 3, Nom = "Resto toro", Telephone = "0492698811" });

			base.Seed(context);
		}
	}
}