using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System.Linq;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
    public class IngredientRepository :BaseRepository, IIngredientRepository
    {
		
		public List<Ingredient> GetAllIngredients()
	    {
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"Select i.Id, i.Name, i.Color, i.Description, typ.Name as 'Effect'
				                     from ingredients i
				                     left outer join typeoptions typ on i.effectType = typ.id ";
				
				var result = con.Query<Ingredient>(query).ToList();
				return result;
			}
		}

	    public List<IngredientMix> GetAllMixes()
	    {
		    using (var con = new SqlConnection(PotionDBConnectionString))
		    {

			    const string query = @"Select i.Id, i.Ingredient1, i.Ingredient2, i.MoodType, i.EffectType
										,i.IsFatal
				                     from ingredientmixes i
				                     left outer join typeoptions typ on i.effectType = typ.id ";

			    var result = con.Query<IngredientMix>(query).ToList();
			    return result;
		    }
		}



    }
}
