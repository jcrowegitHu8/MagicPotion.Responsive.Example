using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public interface IIngredientRepository
	{
		List<Ingredient> GetAllIngredients();
	}
}