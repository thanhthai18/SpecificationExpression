using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.SpecificationExpression
{
    public class SpecificationExpressionExample : MonoBehaviour
    {
        private void Start()
        {
            Execute();
        }

        public void Execute()
        {
            var inventory = new List<ItemExample> { /* ... data ... */ };

            var rareSpec = new ItemRaritySpec(4);
            var cursedSpec = new ItemNameContainsSpec("Cursed");
        
            var finalSpec = rareSpec.And(cursedSpec.Not());

            var results = inventory.AsQueryable()
                                   .Where(finalSpec.ToExpression())
                                   .ToList();

            ItemExample newItem = new ItemExample { name = "Holy Sword", rarity = 5 };
            if (finalSpec.IsSatisfiedBy(newItem))
            {
                Debug.Log("Auto-loot nice item!");
            }
        }
    }
}