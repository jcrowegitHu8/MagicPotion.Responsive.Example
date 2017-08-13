using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagicPotion.Objects;
using MagicPotion.Repository;

namespace MagicPotion.Business
{
	public class IngredientManager : IIngredientManager
	{
		private readonly IIngredientRepository _potionRepo;
		private readonly ITypeOptionRepository _typeOptionRepo;

		public IngredientManager(IIngredientRepository potionRepo, ITypeOptionRepository typeOptionRepo)
		{
			_potionRepo = potionRepo;
			_typeOptionRepo = typeOptionRepo;
		}

		public IEnumerable<Ingredient> GetAllIngredients()
		{
			return _potionRepo.GetAllIngredients();
		}

		public IEnumerable<IngredientMix> GetAllMixes()
		{
			return _potionRepo.GetAllMixes();
		}

		public IEnumerable<Mood> GetAllMoods()
		{
			return _typeOptionRepo.GetAllMoods();
		}

		public IEnumerable<Effect> GetAllEffects()
		{
			return _typeOptionRepo.GetAllEffects();
		}

		public IngredientMixingResult Mix(int moodId, int ingredientId1, int ingredientId2)
		{
			var result = new IngredientMixingResult();
			var mixes = _potionRepo.GetAllMixes();
			IngredientMix aMix;

			if (IsAnIngredientFatal(ingredientId1, ingredientId2, mixes))
			{
				result.Message = @"You mixed a fatal substance and died.  Please be more careful next time. ¯\_(ツ)_/¯ ";
				return result;
			}

			if (!TryGetMix(ingredientId1, ingredientId2, mixes, out aMix))
			{

				if (!TryGetMix(ingredientId2, ingredientId2, mixes, out aMix))
				{
					result.Message = @"BOOM! ¯\_(ツ)_/¯ Looks like an explosion happened. ";
					return result;
				}
			}

			result.SafeMix = true;
			if (moodId == 1)
			{
				result.Message = $"You just leveled up your: {aMix.Effect}";
			}
			else
			{
				result.Message = $"You just exacerbated your: {aMix.Effect}";
			}
			return result;
		}

		public bool IsAnIngredientFatal(int ingredient1Id, int ingredient2Id, IEnumerable<IngredientMix> mixes)
		{
			var result = mixes.FirstOrDefault(o => (o.Ingredient1 == ingredient1Id && o.IsFatal)
												   || (o.Ingredient2 == ingredient2Id && o.IsFatal));
			if (result != null)
			{
				return true;
			}
			return false;
		}

		private bool TryGetMix(int ingredient1Id, int ingredient2Id, IEnumerable<IngredientMix> mixes, out IngredientMix mix)
		{
			mix = mixes.FirstOrDefault(o => o.Ingredient1 == ingredient1Id
		   && o.Ingredient2 == ingredient2Id);

			if (mix != null)
			{
				return true;
			}

			return false;
		}
	}
}
