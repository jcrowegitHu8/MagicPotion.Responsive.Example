using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicPotion.Web.Models.Post
{
	public class IngredientMixingPostModel
	{
		public int MoodId { get; set; }
		public int IngredientId1 { get; set; }
		public int IngredientId2 { get; set; }

	}
}