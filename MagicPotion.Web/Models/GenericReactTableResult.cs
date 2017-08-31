using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MagicPotion.Web.Models
{
	

	public class GenericReactTableResult
	{
		public GenericReactTableResult(GenericReactGrid grid, int sofaId = 0, string updateContainer = null)
		{
			UpdateContainer = updateContainer;

			Grid = new List<GenericReactGrid>();
			if (grid != null)
			{
				Grid.Add(grid);
			}
		}


		[JsonProperty(PropertyName = "updateContainer")]
		public string UpdateContainer { get; set; }

		[JsonProperty(PropertyName = "grid")]
		public List<GenericReactGrid> Grid { get; set; }
	}
}