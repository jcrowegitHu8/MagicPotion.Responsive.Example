using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Configuration;
using System.Linq;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
    public class IngredientRepository : IIngredientRepository
    {
	    private string PotionDBConnectionString => ConfigurationManager.ConnectionStrings["potionsDB"].ConnectionString;
		
		public List<Ingredient> GetAllIngredients()
	    {
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = "Select * from ingredients";
				
				var result = con.Query<Ingredient>(query).ToList();
				return result;
			}
		}

	    public void GetAllMixes()
	    {
	    }



    }
}
