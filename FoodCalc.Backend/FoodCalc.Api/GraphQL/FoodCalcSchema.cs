﻿using System;
using FoodCalc.Api.GraphQL.Types;
using GraphQL;
using GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace FoodCalc.Api.GraphQL
{
    public class FoodCalcSchema : Schema
    {
        public FoodCalcSchema(IDependencyResolver resolver) : base(resolver)
        {
            this.Query = resolver.Resolve<FoodCalcQuery>();
            this.Mutation = resolver.Resolve<FoodCalcMutation>();
        }
    }
}