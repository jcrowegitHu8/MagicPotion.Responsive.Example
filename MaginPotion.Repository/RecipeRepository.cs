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
	public class RecipeRepository : BaseRepository, IRecipeRepository
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

		public Recipe GetRecipe(int id)
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"SELECT r.Id, r.Name, r.EffectType, r.MoodType, r.IsFatal
										,mt.Name as Mood ,et.Name as Effect
										FROM recipes r
										left outer join typeoptions mt on r.effectType = mt.id
										left outer join typeoptions et on r.MoodType = et.id
										WHERE r.id = @id
									 
										SELECT ri.Id, ri.Predecessor, ri.IngredientId, ri.Recipeid
										,i.Name
										FROM RecipeIngredients ri 
										INNER JOIN Ingredients i on ri.IngredientId = i.Id
										WHERE ri.recipeId = @id
									 ";

				var batchQuery = con.QueryMultiple(query, new {id});

				var recipies = batchQuery.Read<Recipe>().ToList();

				var ingredients = batchQuery.Read<RecipeIngredient>().ToList();
				var result = _mapper.Map(ingredients, recipies);

				return result.FirstOrDefault();
			}
		}

		public Recipe UpsertRecipe(Recipe recipe)
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{
				const string query = @"MERGE INTO recipes AS Target
                                        USING (VALUES
                                                        (@Id,@Description,@Name,@IsFatal,@MoodType,@EffectType)
                                            ) AS Source ([Id],[Description],[Name],[IsFatal],[MoodType],[EffectType])
                                            ON (Target.[Id] = Source.[Id])
                                        WHEN MATCHED THEN
                                             UPDATE SET
                                             [Description] = Source.[Description]
                                             ,[Name] = Source.[Name]
                                             ,[IsFatal] = Source.[IsFatal]
                                             ,[MoodType] = Source.[MoodType]
											 ,[EffectType] = Source.[EffectType]
                                        WHEN NOT MATCHED BY TARGET THEN
                                             INSERT([Description],[Name],[IsFatal],[MoodType],[EffectType])
                                             VALUES(Source.[Description],Source.[Name],Source.[IsFatal],[MoodType],[EffectType]);
                                        Select top 1 * From recipes 
										Where ID = (SELECT CASE WHEN SCOPE_IDENTITY() is NULL
                                                    THEN @Id
                                                    ELSE SCOPE_IDENTITY()
                                                    END);";

				var result = con.QuerySingleOrDefault<Recipe>(query
					, new
					{
						recipe.Id,
						recipe.Name,
						recipe.MoodType,
						recipe.EffectType,
						recipe.IsFatal
					});

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
