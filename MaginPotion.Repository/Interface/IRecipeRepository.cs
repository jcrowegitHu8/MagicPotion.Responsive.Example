using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public interface IRecipeRepository
	{
		IEnumerable<Recipe> GetAllRecipes();
	}
}