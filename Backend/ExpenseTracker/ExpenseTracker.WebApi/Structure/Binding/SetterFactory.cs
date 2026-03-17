using System.Linq.Expressions;
using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding
{
    public static class SetterFactory
    {
        public static Action<object, object?> Create(PropertyInfo propertyInfo)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof (object), "value");

            var instanceCast = Expression.Convert(instance, propertyInfo.DeclaringType!);
            var valueCast = Expression.Convert(value, propertyInfo.PropertyType);

            var property = Expression.Property(instanceCast, propertyInfo);
            var assing = Expression.Assign(property, valueCast);

            return Expression
                .Lambda<Action<object, object?>>(assing, instance, value)
                .Compile();
        }
    }
}
