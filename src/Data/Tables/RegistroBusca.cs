using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System;

namespace Dasa.Data.Tables { 

    [Table("RegistroBusca")]
    public class RegistroBusca
    {

        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]        
        public DateTime DataHora { get; set; }

        [Required()]        
        [StringLength(50)]        
        public string NomeSiteOrigem { get; set; }

        [ForeignKey("BuscaId")]
        public ICollection<Roupas> Roupas { get; set; }

    }

}