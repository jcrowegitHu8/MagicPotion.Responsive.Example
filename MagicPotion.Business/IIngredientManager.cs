using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IIngredientManager
	{
		List<Ingredient> GetAllIngredients();
	}
}