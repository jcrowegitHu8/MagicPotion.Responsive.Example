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

			    const string query = @"Select distinct i.Id, i.Ingredient1, i.Ingredient2, i.MoodType, i.EffectType
										,i.IsFatal
									    ,i1.Name as Ingredient1Name
									    ,i2.Name as Ingredient2Name
									    ,moodType.Name as Mood
										,effectType.Name as Effect
				                     from ingredientmixes i
									 left outer join ingredients i1 on i1.id = i.Ingredient1
									 left outer join ingredients i2 on i2.id = i.Ingredient2
				                     left outer join typeoptions moodType on i.MoodType = moodType.id and moodType.type = 'Mood'
				                     left outer join typeoptions effectType on i.effectType = effectType.id and effectType.type = 'Effect'
									 ";

			    var result = con.Query<IngredientMix>(query).ToList();
			    return result;
		    }
		}



    }
}
