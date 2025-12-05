using System;
using System.Linq.Expressions;

namespace Runtime.SpecificationExpression
{
    public class ItemRaritySpec : Specification<ItemExample>
    {
        private readonly int _minRarity;
        public ItemRaritySpec(int minRarity) => _minRarity = minRarity;

        public override Expression<Func<ItemExample, bool>> ToExpression()
        {
            return item => item.rarity >= _minRarity;
        }
    }

    public class ItemNameContainsSpec : Specification<ItemExample>
    {
        private readonly string _keyword;
        public ItemNameContainsSpec(string keyword) => _keyword = keyword;

        public override Expression<Func<ItemExample, bool>> ToExpression()
        {
            return item => item.name.Contains(_keyword);
        }
    }

    public class EnemyIsAggressiveSpec : Specification<EnemyExample>
    {
        public override Expression<Func<EnemyExample, bool>> ToExpression()
        {
            return enemy => enemy.aggroRange > 10 && enemy.damage > 50;
        }
    }
}