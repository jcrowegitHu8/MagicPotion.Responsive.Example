// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MagicPotion.Web.DependencyResolution {
	using AutoMapper;
	using MagicPotion.Business;
	using MagicPotion.Repository;
	using MagicPotion.Web.App_Start;
	using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
	
    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            For<IIngredientManager>().Use<IngredientManager>();
	        For<IIngredientRepository>().Use<IngredientRepository>();
	        For<ITypeOptionRepository>().Use<TypeOptionRepository>();
	        For<IIngredientMixingManager>().Use<IngredientMixingManager>();

	        //For each Profile, include that profile in the MapperConfiguration
	        var config = AutoMapperConfig.RegisterMappings();
	        //Create a mapper that will be used by the DI container
	        var mapper = config.CreateMapper();

	        For<IMapper>().Use(mapper);
		}

        #endregion
    }
}