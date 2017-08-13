using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IIngredientManager
	{
		IEnumerable<Ingredient> GetAllIngredients();
		IEnumerable<IngredientMix> GetAllMixes();
		IEnumerable<Effect> GetAllEffects();
		IEnumerable<Mood> GetAllMoods();
		IngredientMixingResult Mix(int moodId, int ingredientId1, int ingredientId2);
		Ingredient UpsertIngredient(Ingredient ingredient);
	}
}