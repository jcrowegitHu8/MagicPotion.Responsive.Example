using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicPotion.Repository
{
	public abstract class BaseRepository
	{
		protected string PotionDBConnectionString => ConfigurationManager.ConnectionStrings["potionsDB"].ConnectionString;
	}
}
