namespace ExpenseTracker.WebApi.Structure.Binding.Converters
{
    /// <summary>
    /// Classe responsável por converter valores dinâmicos para um tipo específico.
    /// Utilizada no processo de binding para transformar dados recebidos em tipos corretos.
    /// </summary>
    public static class TypeConverter
    {
         /// <summary>
         /// Converte um valor genérico para o tipo informado.
         /// 
         /// Regras de conversão:
         /// - Retorna null caso o valor seja nulo ou vazio
         /// - Suporta conversão para Guid
         /// - Suporta conversão para List<string> (separado por vírgula)
         /// - Suporta conversão para Enum (case insensitive)
         /// - Para outros tipos, utiliza Convert.ChangeType
         /// </summary>
         /// <param name="value">Valor a ser convertido</param>
         /// <param name="type">Tipo de destino</param>
         /// <returns>Valor convertido para o tipo especificado</returns>
        public static object? ConvertTo(object? value, Type type)
        {
            // Retorna null caso o valor seja nulo
            if (value is null) return null;

            // Obtém o tipo base caso seja Nullable<T>
            var targetType = Nullable.GetUnderlyingType(type) ?? type;

            // Converte o valor para string para padronizar o processamento
            var stringValue = value.ToString();

            // Retorna null caso a string esteja vazia ou em branco
            if (string.IsNullOrWhiteSpace(stringValue)) return null;

            // Conversão específica para Guid
            if (targetType == typeof(Guid))
                return Guid.Parse(stringValue);

            // Conversão para lista de strings separadas por vírgula
            if (targetType == typeof(List<string>))
                return stringValue
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .ToList();

            // Conversão para Enum (ignorando maiúsculas/minúsculas)
            if (targetType.IsEnum)
                return Enum.Parse(targetType, stringValue, true);

            // Conversão padrão para outros tipos
            return Convert.ChangeType(stringValue, targetType);
        }
    }
}
