using System.Collections.Generic;

namespace Dasa.CrossCutting.DTO
{    
    public class ResultadoScrap
    {         
        public string Origem { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }        
        public string UrlProduto { get; set; }        
        public string UrlImagem { get; set; }        
        public string Categoria { get; set; }
        public List<string> Tamanhos {get;set;}

    }

}