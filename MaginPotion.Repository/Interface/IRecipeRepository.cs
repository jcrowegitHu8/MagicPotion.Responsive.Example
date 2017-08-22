using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public interface IRecipeRepository
	{
		IEnumerable<Recipe> GetAllRecipes();
		Recipe GetRecipe(int id);
		Recipe UpsertRecipe(Recipe recipe);
		bool DeleteRecipe(int id);
	}
}