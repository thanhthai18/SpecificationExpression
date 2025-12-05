using System;
using System.Linq.Expressions;

namespace Runtime.SpecificationExpression
{
    public enum ComparisonOperator { Equal, GreaterThan, LessThan, Contains }
    
    public class DynamicPropertySpec<T> : Specification<T>
    {
        private readonly string _propertyName;
        private readonly object _value;
        private readonly ComparisonOperator _op;

        public DynamicPropertySpec(string propertyName, ComparisonOperator op, object value)
        {
            _propertyName = propertyName;
            _op = op;
            _value = value;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            // Parameter: (x => ...)
            var param = Expression.Parameter(typeof(T), "x");

            // Property: x.PropertyName
            MemberExpression propExpr = Expression.PropertyOrField(param, _propertyName);

            // Value: Constant(_value)
            var targetType = propExpr.Type;
            var convertedValue = Convert.ChangeType(_value, targetType);
            ConstantExpression valExpr = Expression.Constant(convertedValue);

            Expression body = null;

            switch (_op)
            {
                case ComparisonOperator.Equal:
                    body = Expression.Equal(propExpr, valExpr);
                    break;
                case ComparisonOperator.GreaterThan:
                    body = Expression.GreaterThan(propExpr, valExpr);
                    break;
                case ComparisonOperator.LessThan:
                    body = Expression.LessThan(propExpr, valExpr);
                    break;
                case ComparisonOperator.Contains:
                    var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    body = Expression.Call(propExpr, method, valExpr);
                    break;
            }

            return Expression.Lambda<Func<T, bool>>(body, param);
        }

        public void HowToUse()
        {
//             string configProp = "Health";
//             string configOp = "LessThan";
//             int configVal = 50;
//
//             var lowHealthSpec = new DynamicPropertySpec<Enemy>(configProp, ComparisonOperator.LessThan, configVal);
//
//             var weakEnemies = allEnemies.Where(lowHealthSpec.ToExpression().Compile()).ToList();
        }
    }
}