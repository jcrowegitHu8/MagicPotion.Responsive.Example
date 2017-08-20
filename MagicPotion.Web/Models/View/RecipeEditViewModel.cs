using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagicPotion.Objects;

namespace MagicPotion.Web.Models.View
{
	public class RecipeEditViewModel
	{
		public Recipe Recipe { get; set; }
		public IEnumerable<Mood> Moods { get; set; }
		public IEnumerable<Effect> Effects { get; set; }
		public IEnumerable<Ingredient> Ingredients { get; set; }
	}
}