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

		public bool DeleteRecipe(int id)
		{
			var result = _recipeRepo.DeleteRecipe(id);
			return result;
		}

		public IEnumerable<Recipe> GetAllRecipes()
		{
			var result = _recipeRepo.GetAllRecipes();
			return result;
		}

		public Recipe GetRecipe(int id)
		{
			var result = _recipeRepo.GetRecipe(id);
			return result;
		}

		public Recipe UpsertRecipe(Recipe recipe)
		{
			var result = _recipeRepo.UpsertRecipe(recipe);
			return result;
		}
	}
}
