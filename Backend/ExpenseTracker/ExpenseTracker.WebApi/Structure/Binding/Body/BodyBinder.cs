using System.Reflection;

namespace ExpenseTracker.WebApi.Structure.Binding.Body
{
    /// <summary>
    /// Responsável por realizar o binding do corpo da requisição (Body)
    /// para uma propriedade do endpoint.
    ///
    /// Suporta requisições com conteúdo JSON, realizando a desserialização
    /// automática para o tipo da propriedade informada.
    /// Utilizado pelo EndpointBinder para complementar o binding de dados,
    /// tratando especificamente o corpo da requisição.
    /// </summary>
    public static class BodyBinder
    {
        /// <summary>
        /// Realiza o binding do body da requisição para a propriedade do endpoint.
        /// </summary>
        /// <param name="endpoint">Instância do endpoint</param>
        /// <param name="context">Contexto HTTP da requisição</param>
        /// <param name="property">Propriedade que receberá o valor do body</param>
        /// <param name="cancellationToken">Token de cancelamento</param>
        public static async Task BindAsync(object endpoint, HttpContext context, PropertyInfo property, CancellationToken cancellationToken)
        {
            // Verifica se o conteúdo da requisição é JSON
            if (!context.Request.HasJsonContentType()) return;

            // Desserializa o corpo da requisição para o tipo da propriedade
            var body = await context.Request.ReadFromJsonAsync(property.PropertyType, cancellationToken);

            // Atribui o valor desserializado à propriedade do endpoint
            if (body is not null)
                property.SetValue(endpoint, body);
        }
    }
}
