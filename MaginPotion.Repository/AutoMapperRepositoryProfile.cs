using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MagicPotion.Objects;
using MagicPotion.Repository.DBTO;
using Newtonsoft.Json;

namespace MagicPotion.Repository
{
	public class AutoMapperRepositoryProfile : Profile
	{
		public AutoMapperRepositoryProfile() {

			CreateMap<RecipeDBTO, Recipe>()
				.ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => 
				JsonConvert.DeserializeObject<List<RecipeIngredient>>(src.Ingredients)));

			CreateMap<Recipe, RecipeDBTO>()
				.ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
					JsonConvert.SerializeObject(src.Ingredients)));

			CreateMap<IngredientMix, RecipeIngredient>().ReverseMap();
			//var converter = DynamicCollectionConverterWithIdMatchingAndPropertySpecified<RecipeIngredient, Recipe>
			//	.Instance(ri => ri.RecipeId, r => r.Id, dest => dest.Ingredients);
			//CreateMap<List<RecipeIngredient>, List<Recipe>>().ConvertUsing(converter);
		}
	}

	

	/// <summary>
	/// This can take two list of object and wire them together
	/// base on specified properties and will add the matched objects
	/// to the specified list property.
	/// </summary>
	/// <typeparam name="TSource"></typeparam>
	/// <typeparam name="TDestination"></typeparam>
	public class DynamicCollectionConverterWithIdMatchingAndPropertySpecified<TSource, TDestination> :
		ITypeConverter<List<TSource>, List<TDestination>> where TDestination : class
	{
		private readonly Func<TSource, object> sourcePrimaryKeyExpression;
		private readonly Func<TDestination, object> destinationPrimaryKeyExpression;
		private readonly Func<TDestination, object> destinationListPropertyExpression;
		private readonly Type destinationType;


		private DynamicCollectionConverterWithIdMatchingAndPropertySpecified(Expression<Func<TSource, object>> sourcePrimaryKey
			, Expression<Func<TDestination, object>> destinationPrimaryKey
			, Expression<Func<TDestination, object>> destinationListProperty)
		{
			this.sourcePrimaryKeyExpression = sourcePrimaryKey.Compile();
			this.destinationPrimaryKeyExpression = destinationPrimaryKey.Compile();
			this.destinationListPropertyExpression = destinationListProperty.Compile();
			this.destinationType = GetPropertyType(destinationListProperty);
		}

		public static Type GetPropertyType(Expression<Func<TDestination, object>> expression)
		{
			var body = expression.Body as MemberExpression;
			if (body == null)
			{
				throw new ArgumentException("'expression' should be a member expression");
			}
			return ((PropertyInfo)body.Member).PropertyType;
		}

		public static DynamicCollectionConverterWithIdMatchingAndPropertySpecified<TSource, TDestination>
			Instance(Expression<Func<TSource, object>> sourcePrimaryKey
			, Expression<Func<TDestination, object>> destinationPrimaryKey
			, Expression<Func<TDestination, object>> destinationListProperty)
		{
			return new DynamicCollectionConverterWithIdMatchingAndPropertySpecified<TSource, TDestination>(
				sourcePrimaryKey, destinationPrimaryKey, destinationListProperty);
		}

		private string GetPrimaryKey<TObject>(object entity, Func<TObject, object> expression)
		{
			var tempId = expression.Invoke((TObject)entity);
			var id = System.Convert.ToString(tempId);
			return id;
		}

		private IList GetDestinationProperty<TObject>(object entity, Func<TObject, object> expression)
		{
			var temp = expression.Invoke((TObject)entity);
			if (temp == null)
			{
				//temp = (IList)Activator.CreateInstance(this.destinationType);
			}
			return (IList)temp;
		}

		

		public List<TDestination> Convert(List<TSource> sources, List<TDestination> destinations, ResolutionContext context)
		{
			foreach (var sourceItem in sources)
			{
				TDestination matchedDestination = default(TDestination);

				foreach (var destinationItem in destinations)
				{
					
					var sourcePrimaryKey = GetPrimaryKey(sourceItem, this.sourcePrimaryKeyExpression);
					var destinationPrimaryKey = GetPrimaryKey(destinationItem, this.destinationPrimaryKeyExpression);

					if (string.Equals(sourcePrimaryKey, destinationPrimaryKey, StringComparison.OrdinalIgnoreCase))
					{
						var destinationProperty = GetDestinationProperty(destinationItem, this.destinationListPropertyExpression);
						destinationProperty.Add(sourceItem);
						break;
					}
				}
			}

			return destinations;
		}
	}

	public static class Extensions
	{
		

	}
}
