using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public interface IIngredientRepository
	{
		Ingredient GetIngredient(int id);
		List<Ingredient> GetAllIngredients();
		List<IngredientMix> GetAllMixes();
		Ingredient UpsertIngredient(Ingredient ingredient);
	}
}