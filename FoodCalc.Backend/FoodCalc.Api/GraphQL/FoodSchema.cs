using System;
using FoodCalc.Api.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace FoodCalc.Api.GraphQL
{
    public class FoodSchema : Schema
    {
        public FoodSchema(IDependencyResolver resolver) : base(resolver)
        {
            this.Query = resolver.Resolve<FoodQuery>();
        }
    }
}