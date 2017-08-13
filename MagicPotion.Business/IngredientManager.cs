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

		

		public Ingredient UpsertIngredient(Ingredient ingredient)
		{
			return _potionRepo.UpsertIngredient(ingredient);
		}
	}
}
