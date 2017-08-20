using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	/// <summary>
	/// This is really just a container for
	/// all of the ingredients to be specified for the mix.
	/// </summary>
	public class Recipe
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsFatal { get; set; }
		public int MoodType { get; set; }
		public string Mood { get; set; }
		public int EffectType { get; set; }
		public string Effect { get; set; }

		public List<RecipeIngredient> Ingredients { get; set; } = new List<RecipeIngredient>();
	}

	/// <summary>
	/// Ordered Ingredients for a Recipe.
	/// </summary>
	public class RecipeIngredient
	{
		/// <summary>
		/// This indicates our order of items.
		/// </summary>
		public int? Predecessor { get; set; }
		public int RecipeId { get; set; }
		public int IngredientId { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
