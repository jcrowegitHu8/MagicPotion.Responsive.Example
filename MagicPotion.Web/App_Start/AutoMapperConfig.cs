using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MagicPotion.Objects;
using MagicPotion.Repository;
using MagicPotion.Web.Models.Post;
using MagicPotion.Web.Models.View;

namespace MagicPotion.Web.App_Start
{
	public static class AutoMapperConfig
	{
		public static MapperConfiguration MapperConfiguration;

		public static MapperConfiguration RegisterMappings()
		{
			return new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new AutoMapperRepositoryProfile());

				cfg.CreateMap<IngredientPostModel, Ingredient>().ReverseMap();

				cfg.CreateMap<IngredientMixingPostModel, IngredientMix>().ReverseMap();
				//.ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => src.ContentType.ToString()));

				cfg.CreateMap<RecipesViewModel, Recipe>().ReverseMap();

			});
		}
	}
}