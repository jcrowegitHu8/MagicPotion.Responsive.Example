using System.Collections.Generic;
using System.IO;
using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IIngredientManager
	{
		Ingredient GetIngredient(int id);
		IEnumerable<Ingredient> GetAllIngredients();
		IEnumerable<IngredientMix> GetAllMixes();
		IEnumerable<Effect> GetAllEffects();
		IEnumerable<Mood> GetAllMoods();
		Ingredient UpsertIngredient(Ingredient ingredient);
		IngredientUploadResult ParseCsvAndUpdateIngredients(Stream fileStream);
	}
}