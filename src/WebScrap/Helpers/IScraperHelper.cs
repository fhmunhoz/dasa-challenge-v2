namespace Dasa.WebScrap.Helpers
{
    public interface IScraperHelper
    {
        string UrlProximaPagina(string urlPaginaAtual, string QuerySTringId);
        decimal TratamentoPreco(string precoTexto);
    }
}

