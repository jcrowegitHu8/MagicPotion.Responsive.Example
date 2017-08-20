using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagicPotion.Objects;

namespace MagicPotion.Web.Models.View
{
	public class RecipesViewModel : Recipe
	{
		public int IngredientCount => Ingredients?.Count() ?? 0;
	}
}