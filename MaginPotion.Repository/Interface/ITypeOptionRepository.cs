using System.Collections.Generic;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public interface ITypeOptionRepository
	{
		IEnumerable<Mood> GetAllMoods();
		IEnumerable<Effect> GetAllEffects();
	}
}