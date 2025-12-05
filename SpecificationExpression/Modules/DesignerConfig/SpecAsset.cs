using System.Collections.Generic;
using UnityEngine;

namespace Runtime.SpecificationExpression
{
    public abstract class SpecAsset<T> : ScriptableObject
    {
        public abstract Specification<T> GetSpec();
    }

    [CreateAssetMenu(menuName = "Specs/Composite/AND Group")]
    public class AndSpecAsset<T> : SpecAsset<T>
    {
        public List<SpecAsset<T>> Conditions;

        public override Specification<T> GetSpec()
        {
            var spec = new TrueSpecification<T>();
            foreach (var condition in Conditions)
            {
                spec = (TrueSpecification<T>)spec.And(condition.GetSpec());
            }
            return spec;
        }
    }
}