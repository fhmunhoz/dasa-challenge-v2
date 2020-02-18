namespace Dasa.WebScrap.Interfaces
{

    public interface IScraperFactory
    {
        IScraperSite RetornaScraperPorNome(string nome);
    }
}