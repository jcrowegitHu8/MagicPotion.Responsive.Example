using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicPotion.Objects;
using MagicPotion.Repository;

namespace MagicPotion.Business
{
	public class RecipeManager : IRecipeManager
	{
		private readonly IRecipeRepository _recipeRepo;

		public RecipeManager(IRecipeRepository recipeRepo)
		{
			_recipeRepo = recipeRepo;
		}

		public IEnumerable<Recipe> GetAllRecipes()
		{
			var result = _recipeRepo.GetAllRecipes();
			return result;
		}
	}
}
