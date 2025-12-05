using System;
using System.Collections.Generic;

namespace Runtime.SpecificationExpression
{
    public class SearchQueryBuilder<T>
    {
        private Specification<T> _finalSpec = new TrueSpecification<T>();

        public SearchQueryBuilder<T> AddOrGroup(IEnumerable<Specification<T>> specs)
        {
            Specification<T> groupSpec = new FalseSpecification<T>(); 
            foreach (var spec in specs)
            {
                groupSpec = groupSpec.Or(spec);
            }

            _finalSpec = _finalSpec.And(groupSpec);
            return this;
        }

        public SearchQueryBuilder<T> AddFilter(Specification<T> spec)
        {
            _finalSpec = _finalSpec.And(spec);
            return this;
        }

        public SearchQueryBuilder<T> Exclude(Specification<T> spec)
        {
            _finalSpec = _finalSpec.And(spec.Not());
            return this;
        }

        public Func<T, bool> Build()
        {
            return _finalSpec.ToExpression().Compile();
        }

        public void HowToUse()
        {
//             var builder = new SearchQueryBuilder<Card>();
//
//             var elementSpecs = new List<Specification<Card>> { new ElementSpec("Fire"), new ElementSpec("Ice") };
//             builder.AddOrGroup(elementSpecs);
//
//             builder.AddFilter(new MinAttackSpec(50));
//
//             builder.Exclude(new TypeSpec("Spell"));
//
//             var filterFunc = builder.Build();
//             var results = allCards.Where(filterFunc).ToList();
        }
    }

    public class TrueSpecification<T> : Specification<T>
    {
        public override System.Linq.Expressions.Expression<Func<T, bool>> ToExpression() => x => true;
    }

    public class FalseSpecification<T> : Specification<T>
    {
        public override System.Linq.Expressions.Expression<Func<T, bool>> ToExpression() => x => false;
    }
}