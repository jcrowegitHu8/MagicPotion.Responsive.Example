namespace MagicPotion.Web.Models.Post
{
	public class IngredientPostModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Color { get; set; }
		public string Description { get; set; }
		public string Effect { get; set; }
		public int EffectType { get; set; }
		public string ImportId { get; set; }
	}
}