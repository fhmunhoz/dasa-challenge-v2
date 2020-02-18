using System.Threading.Tasks;
using System.Collections.Generic;
using Dasa.Catalogo.Helpers;
using Dasa.Catalogo.Models;

namespace Dasa.Catalogo.Interfaces
{

    public interface IBusca
    {
        Task<PagingHelper<BuscaViewModel>> CompararProdutos(string termoBusca, int paginaAtual, int itensPagina);

    }

}