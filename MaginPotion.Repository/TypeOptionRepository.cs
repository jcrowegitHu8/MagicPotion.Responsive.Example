using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MagicPotion.Objects;

namespace MagicPotion.Repository
{
	public class TypeOptionRepository:BaseRepository, ITypeOptionRepository
	{
		public IEnumerable<Mood> GetAllMoods()
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"Select typ.Id, typ.Name
				                     from typeoptions typ 
									 where typ.Type = 'Mood'";

				var result = con.Query<Mood>(query).ToList();
				return result;
			}
		}


		public IEnumerable<Effect> GetAllEffects()
		{
			using (var con = new SqlConnection(PotionDBConnectionString))
			{

				const string query = @"Select typ.Id, typ.Name
				                     from typeoptions typ 
									 where typ.Type = 'Effect'";

				var result = con.Query<Effect>(query).ToList();
				return result;
			}
		}
	}
}
