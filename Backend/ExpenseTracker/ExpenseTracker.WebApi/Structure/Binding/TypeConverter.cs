namespace ExpenseTracker.WebApi.Structure.Binding
{
    public static class TypeConverter
    {
        public static object? ConvertTo(object? value, Type type)
        {
            if (value is null) return null;

            var targetType = Nullable.GetUnderlyingType(type) ?? type;
            var stringValue = value.ToString();

            if (string.IsNullOrWhiteSpace(stringValue)) return null;

            if (targetType == typeof(Guid))
                return Guid.Parse(stringValue);

            if (targetType == typeof(List<string>))
                return stringValue
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

            if (targetType.IsEnum)
                return Enum.Parse(targetType, stringValue, true);

            return Convert.ChangeType(stringValue, targetType);
        }
    }
}
