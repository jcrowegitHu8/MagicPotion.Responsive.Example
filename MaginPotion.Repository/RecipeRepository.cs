using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public class RecipeRepository :BaseRepository, IRecipeRepository
	{
		private readonly IMapper _mapper;

		public RecipeRepository(IMapper mapper)
		{
			_mapper = mapper;
		}

		public IEnumerable<Recipe> GetAllRecipes()
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"SELECT r.Id, r.Name, r.EffectType, r.MoodType, r.IsFatal
										,mt.Name as Mood ,et.Name as Effect
										FROM recipes r
										left outer join typeoptions mt on r.effectType = mt.id
										left outer join typeoptions et on r.MoodType = et.id
									 
										SELECT ri.Id, ri.Predecessor, ri.IngredientId, ri.Recipeid
										,i.Name
										FROM RecipeIngredients ri 
										INNER JOIN Ingredients i on ri.IngredientId = i.Id
									 ";

				var batchQuery = con.QueryMultiple(query);

				var recipies = batchQuery.Read<Recipe>().ToList();

				var ingredients = batchQuery.Read<RecipeIngredient>().ToList();
				var result = _mapper.Map(ingredients, recipies);

				return result;
			}
		}
	}
}
