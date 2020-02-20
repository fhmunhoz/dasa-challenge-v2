using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dasa.Data.Tables
{
    [Table("Roupas")]
    public class Roupas
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        [StringLength(512)]        
        public string Nome { get; set; }

        [Required()]
        [StringLength(5000)]        
        public string Descricao { get; set; }

        [Required()]        
        public decimal Preco { get; set; }

        [Required()]
        [StringLength(512)]        
        public string UrlProduto { get; set; }

        [Required()]
        [StringLength(512)]        
        public string UrlImagem { get; set; }

        [Required()]
        [StringLength(50)]        
        public string Categoria { get; set; }

        [ForeignKey("RoupaId")]
        public ICollection<RoupasTamanho> Tamanhos { get; set; }

        [Required()]
        public int BuscaId { get; set; }

    }

}