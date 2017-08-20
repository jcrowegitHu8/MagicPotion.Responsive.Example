using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IRecipeManager
	{
		IEnumerable<Recipe> GetAllRecipes();
		Recipe GetRecipe(int id);
		Recipe UpsertRecipe(Recipe recipe);
	}
}