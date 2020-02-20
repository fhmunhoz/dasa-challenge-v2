using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dasa.Data.Tables
{
    [Table("RegistroScraping")]
    public class RegistroScraping
    {
        
        [Key]
        [Required()]
        [StringLength(255)]
        public string NomeSite { get; set; }
        
        public DateTime? DataUltimoScraping { get; set; }

        [Required()]
        public bool Ativo { get; set; }

        [Required()]
        public bool EmProcessamento { get; set; }

    }

}