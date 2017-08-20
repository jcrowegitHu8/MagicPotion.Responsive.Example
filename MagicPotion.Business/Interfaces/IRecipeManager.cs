using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IRecipeManager
	{
		IEnumerable<Recipe> GetAllRecipes();
	}
}