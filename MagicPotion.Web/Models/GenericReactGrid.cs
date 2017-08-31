using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MagicPotion.Web.Models
{
	public class GenericReactGrid
	{
		public GenericReactGrid(IEnumerable<string> columnHeaders, int dataId)
		{
			DataId = dataId;
			GridHeader = columnHeaders.Select(h => new GenericReactTableHeader { Header = h }).ToList();
			GridRow = new List<Dictionary<string, object>>();
		}
		public GenericReactGrid(int dataId)
		{
			DataId = dataId;
			GridHeader = new List<GenericReactTableHeader>();
			GridRow = new List<Dictionary<string, object>>();
		}

		public int DataId { get; set; }

		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		[JsonProperty(PropertyName = "gridheader")]
		public List<GenericReactTableHeader> GridHeader { get; set; }

		[JsonProperty(PropertyName = "gridrow")]
		public List<Dictionary<string, object>> GridRow { get; set; }

		public int NumberOfColumns => GridHeader.Count;

		public int NumberOfRows => GridRow.Count;
	}

	public class GenericReactTableHeader
	{
		[JsonProperty(PropertyName = "header")]
		public string Header { get; set; }

		/// <summary>
		/// Only used if sub grid
		/// </summary>
		public string Width { get; set; }
	}
}