﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FakerLibrary.generators;

namespace FakerLibrary.faker
{
    public class FakerConfiguration
    {
        private List<FakerGeneratorRule> FakerGeneratorRules { get; }

        public FakerConfiguration(List<FakerGeneratorRule> fakerGeneratorRules)
        {
            FakerGeneratorRules = fakerGeneratorRules;
        }

        public void AddFakerRule<T, TK, TD>(Expression<Func<T, TK>> fakerRule) where TD: IGenerator<TK>
        {
            var fakerGeneratorRule = new FakerGeneratorRule((((MemberExpression)fakerRule.Body).Member.Name),typeof(T), typeof(TK), typeof(TD));
            FakerGeneratorRules.Add(fakerGeneratorRule);
        }
    }
}