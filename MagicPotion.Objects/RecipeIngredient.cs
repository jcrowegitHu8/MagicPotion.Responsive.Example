using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Objects
{
	/// <summary>
	/// Ordered Ingredients for a Recipe.
	/// </summary>
	public class RecipeIngredient
	{
		public int IngredientId { get; set; }
		/// <summary>
		/// A copy of the name for display purposes.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// This indicates our order of items.
		/// </summary>
		public int? Predecessor { get; set; }

	}
}
