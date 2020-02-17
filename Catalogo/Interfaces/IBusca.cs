using System.Threading.Tasks;
using Dasa.Catalogo.Domain;
using System.Collections.Generic;

namespace Dasa.Catalogo.Interfaces
{

    public interface IBusca
    {
        Task<Helpers.PagingHelper<BuscaViewModel>> CompararProdutos(string termoBusca, int paginaAtual, int itensPagina);

    }

}