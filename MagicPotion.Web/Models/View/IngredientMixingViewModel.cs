using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MagicPotion.Objects;

namespace MagicPotion.Web.Models.View
{
	/// <summary>
	/// We have a View model because this will normally be a
	/// heavier object with more stuff for the initial screen to load
	/// drop downs and all kind of other data to provide a simpler user experience.
	/// </summary>
	public class IngredientMixingViewModel
	{
		public IEnumerable<Ingredient> Ingredients { get; set; }
		public IEnumerable<IngredientMix> IngredientMixes { get; set; }
		public IEnumerable<Mood> Moods { get; set; }
		public IEnumerable<Effect> Effects { get; set; }
	}
}