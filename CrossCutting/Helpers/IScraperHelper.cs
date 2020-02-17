namespace Dasa.CrossCutting.Helpers
{
    public interface IScraperHelper
    {
        string UrlProximaPagina(string urlPaginaAtual, string QuerySTringId);
        decimal TratamentoPreco(string precoTexto);
    }
}

