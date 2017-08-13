using MagicPotion.Objects;

namespace MagicPotion.Business
{
	public interface IIngredientMixingManager
	{
		IngredientMixingResult Mix(int moodId, int ingredientId1, int ingredientId2);
	}
}