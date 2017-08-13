using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicPotion.Objects;
using MagicPotion.Repository;

namespace MagicPotion.Business
{
	public class IngredientMixingManager : IIngredientMixingManager
	{
		private readonly IIngredientRepository _potionRepo;

		public IngredientMixingManager(IIngredientRepository potionRepo)
		{
			_potionRepo = potionRepo;
		}

		public IngredientMixingResult Mix(int moodId, int ingredientId1, int ingredientId2)
		{
			var result = new IngredientMixingResult();
			var mixes = _potionRepo.GetAllMixes();
			IngredientMix aMix;

			if (IsAnIngredientFatal(ingredientId1, mixes)
				|| IsAnIngredientFatal(ingredientId2, mixes))
			{
				result.IsMixFatal = true;
				result.IsMixDocumented = true;
				return result;
			}

			if (!TryGetMix(ingredientId1, ingredientId2, moodId, mixes, out aMix))
			{
				if (!TryGetMix(ingredientId2, ingredientId1, moodId, mixes, out aMix))
				{
					result.IsMixFatal = true;
					return result;
				}
			}
			result.IsMixDocumented = true;
			result.Effect = aMix.Effect;
			
			return result;
		}

		public bool IsAnIngredientFatal(int ingredient1Id, IEnumerable<IngredientMix> mixes)
		{
			var result = mixes.FirstOrDefault(o => (o.Ingredient1 == ingredient1Id && o.IsFatal)
			                                       || (o.Ingredient2 == ingredient1Id && o.IsFatal));
			if (result != null)
			{
				return true;
			}
			return false;
		}

		private bool TryGetMix(int ingredient1Id, int ingredient2Id, int mood, IEnumerable<IngredientMix> mixes, out IngredientMix mix)
		{
			mix = mixes.FirstOrDefault(o => o.Ingredient1 == ingredient1Id
			                                && o.Ingredient2 == ingredient2Id
			                                && (o.Mood == null || o.MoodType == mood));

			if (mix != null)
			{
				return true;
			}

			return false;
		}
	}
}
