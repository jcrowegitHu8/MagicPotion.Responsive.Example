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

	    public IngredientManager(IIngredientRepository potionRepo)
	    {
		    _potionRepo = potionRepo;
	    }

	    public List<Ingredient> GetAllIngredients()
	    {
		    return _potionRepo.GetAllIngredients();
	    }
    }
}
