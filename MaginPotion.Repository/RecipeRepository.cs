using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using MagicPotion.Objects;
using MagicPotion.Repository.DBTO;

namespace MagicPotion.Repository
{
	public class RecipeRepository : BaseRepository, IRecipeRepository
	{
		private readonly IMapper _mapper;

		public RecipeRepository(IMapper mapper)
		{
			_mapper = mapper;
		}

		public bool DeleteRecipe(int id)
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"DELETE FROM recipes WHERE id = @id";

				var rowsAffected = con.Execute(query, new { id });
				if (rowsAffected == 1)
				{
					return true;
				}
				return false;
			}
		}

		public IEnumerable<Recipe> GetAllRecipes()
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"SELECT r.Id, r.Name, r.EffectType, r.MoodType, r.IsFatal
										,mt.Name as Mood ,et.Name as Effect
										,r.Ingredients
										FROM recipes r
										left outer join typeoptions mt on r.effectType = mt.id
										left outer join typeoptions et on r.MoodType = et.id
									 
									 ";

				var dbto = con.Query<RecipeDBTO>(query);
				var result = _mapper.Map<List<Recipe>>(dbto);
				return result;
			}
		}

		public Recipe GetRecipe(int id)
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"SELECT r.Id, r.Name, r.EffectType, r.MoodType, r.IsFatal
										,mt.Name as Mood ,et.Name as Effect
										,r.Ingredients
										FROM recipes r
										left outer join typeoptions mt on r.effectType = mt.id
										left outer join typeoptions et on r.MoodType = et.id
										WHERE r.id = @id

									 ";

				var dbto = con.QuerySingleOrDefault<RecipeDBTO>(query, new { id });
				var result = _mapper.Map<Recipe>(dbto);
				return result;
			}
		}

		public Recipe UpsertRecipe(Recipe recipe)
		{
			var dbto = _mapper.Map<RecipeDBTO>(recipe);

			using (var con = new SqlConnection(PotionDBConnectionString))
			{
				const string query = @"MERGE INTO recipes AS Target
                                        USING (VALUES
                                                        (@Id,@Ingredients,@Name,@IsFatal,@MoodType,@EffectType)
                                            ) AS Source ([Id],[Ingredients],[Name],[IsFatal],[MoodType],[EffectType])
                                            ON (Target.[Id] = Source.[Id])
                                        WHEN MATCHED THEN
                                             UPDATE SET
                                             [Ingredients] = Source.[Ingredients]
                                             ,[Name] = Source.[Name]
                                             ,[IsFatal] = Source.[IsFatal]
                                             ,[MoodType] = Source.[MoodType]
											 ,[EffectType] = Source.[EffectType]
                                        WHEN NOT MATCHED BY TARGET THEN
                                             INSERT([Ingredients],[Name],[IsFatal],[MoodType],[EffectType])
                                             VALUES(Source.[Ingredients],Source.[Name],Source.[IsFatal],[MoodType],[EffectType]);
                                        
										SELECT r.Id, r.Name, r.EffectType, r.MoodType, r.IsFatal
										,mt.Name as Mood ,et.Name as Effect
										,r.Ingredients
										FROM recipes r
										left outer join typeoptions mt on r.effectType = mt.id
										left outer join typeoptions et on r.MoodType = et.id 
										Where r.Id = (SELECT CASE WHEN SCOPE_IDENTITY() is NULL
                                                    THEN @Id
                                                    ELSE SCOPE_IDENTITY()
                                                    END);";

				var tempDbto = con.QuerySingleOrDefault<RecipeDBTO>(query
					, new
					{
						dbto.Id,
						dbto.Name,
						dbto.MoodType,
						dbto.EffectType,
						dbto.IsFatal,
						dbto.Ingredients
					});

				var result = _mapper.Map<Recipe>(tempDbto);
				return result;
			}
		}

		//private Recipe UpsertRecipeIngredients(List<RecipeIngredient> ingredients)
		//{
		//	using (var con = new SqlConnection(PotionDBConnectionString))
		//	{
		//		const string query = @"MERGE INTO recipes AS Target
		//                                      USING (VALUES
		//                                                      (@Id,@Description,@Name,@IsFatal,@MoodType,@EffectType)
		//                                          ) AS Source ([Id],[Description],[Name],[IsFatal],[MoodType],[EffectType])
		//                                          ON (Target.[Id] = Source.[Id])
		//                                      WHEN MATCHED THEN
		//                                           UPDATE SET
		//                                           [Description] = Source.[Description]
		//                                           ,[Name] = Source.[Name]
		//                                           ,[IsFatal] = Source.[IsFatal]
		//                                           ,[MoodType] = Source.[MoodType]
		//									 ,[EffectType] = Source.[EffectType]
		//                                      WHEN NOT MATCHED BY TARGET THEN
		//                                           INSERT([Description],[Name],[IsFatal],[MoodType],[EffectType])
		//                                           VALUES(Source.[Description],Source.[Name],Source.[IsFatal],[MoodType],[EffectType]);
		//                                      Select top 1 * From recipes 
		//								Where ID = (SELECT CASE WHEN SCOPE_IDENTITY() is NULL
		//                                                  THEN @Id
		//                                                  ELSE SCOPE_IDENTITY()
		//                                                  END);";

		//		var result = con.QuerySingleOrDefault<Ingredient>(query
		//			, new
		//			{
		//				recipe.Id,
		//				recipe.Name,
		//				recipe.MoodType,
		//				recipe.EffectType,
		//				recipe.IsFatal
		//			});

		//		return result;
		//	}
		//}
	}
}
