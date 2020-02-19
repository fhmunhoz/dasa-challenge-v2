using System.Collections.Generic;

namespace Dasa.Catalogo.Models
{
    public class BuscaViewModel
    {
        public string Origem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }

        public int PrecoOrdenacao { get; set; }
        public string UrlProduto { get; set; }
        public string UrlImagem { get; set; }
        public string Categoria { get; set; }
        public string Tamanhos { get; set; }
        public bool MenorPreco { get; set; }
        public bool ProdutoNovo { get; set; }

    }

}