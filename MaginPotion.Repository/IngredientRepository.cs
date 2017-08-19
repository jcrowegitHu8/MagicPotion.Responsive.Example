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
	    public Ingredient GetIngredient(int id)
	    {
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"Select i.Id, i.Name, i.Color, i.Description, typ.Name as 'Effect'
									  ,i.ImportId,i.effectType
				                     from ingredients i
				                     left outer join typeoptions typ on i.effectType = typ.id
									 where i.id = @id";

				var result = con.QueryFirstOrDefault<Ingredient>(query, new{id});
				return result;
			}
		}


		public List<Ingredient> GetAllIngredients()
	    {
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"Select i.Id, i.Name, i.Color, i.Description, typ.Name as 'Effect'
									  ,i.ImportId,i.effectType
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

	    public Ingredient UpsertIngredient(Ingredient ingredient)
	    {
			using (var con = new SqlConnection(PotionDBConnectionString))
			{
				const string query = @"MERGE INTO ingredients AS Target
                                        USING (VALUES
                                                        (@Id,@Description,@Name,@Color,@ImportId,@EffectType)
                                            ) AS Source ([Id],[Description],[Name],[Color],[ImportId],[EffectType])
                                            ON (Target.[Id] = Source.[Id])
                                        WHEN MATCHED THEN
                                             UPDATE SET
                                             [Description] = Source.[Description]
                                             ,[Name] = Source.[Name]
                                             ,[Color] = Source.[Color]
                                             ,[ImportId] = Source.[ImportId]
											 ,[EffectType] = Source.[EffectType]
                                        WHEN NOT MATCHED BY TARGET THEN
                                             INSERT([Description],[Name],[Color],[ImportId],[EffectType])
                                             VALUES(Source.[Description],Source.[Name],Source.[Color],[ImportId],[EffectType]);
                                        Select top 1 * From ingredients 
										Where ID = (SELECT CASE WHEN SCOPE_IDENTITY() is NULL
                                                    THEN @Id
                                                    ELSE SCOPE_IDENTITY()
                                                    END);";

				var result = con.QuerySingleOrDefault<Ingredient>(query
					, new
					{
						ingredient.Id,
						ingredient.Name,
						ingredient.Description,
						ingredient.Color,
						EffectType = ingredient.EffectType,
						ingredient.ImportId
					});

				return result;
			}
		}




	}
}
