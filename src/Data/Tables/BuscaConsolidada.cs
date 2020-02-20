using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dasa.Data.Tables
{
    [Table("BuscaConsolidada")]
    public class BuscaConsolidada
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        [StringLength(512)]        
        public string UrlProduto { get; set; }

        [Required()]
        [StringLength(512)]        
        public string Nome { get; set; }

        [Required()]
        [StringLength(1024)]        
        public string Descricao { get; set; }

        [Required()]        
        public decimal Preco { get; set; }

        [Required()]        
        public int PrecoOrdenacao { get; set; }

        [Required()]
        [StringLength(255)]        
        public string UrlImagem { get; set; }

        [Required()]
        [StringLength(50)]        
        public string Categoria { get; set; }

        [Required()]
        [StringLength(50)]        
        public string Origem { get; set; }

        [Required()]
        [StringLength(255)]        
        public string Tamanhos { get; set; }

        [Required()]        
        public bool MenorPreco { get; set; }

        [Required()]
        public bool MaiorPreco { get; set; }

        [Required()]
        public bool ProdutoNovo { get; set; }

    }

}