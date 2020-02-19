using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dasa.Data.Tables
{
    [Table("RoupasTamanho")]
    public class RoupasTamanho
    {

        [Key]        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required()]
        public int RoupaId { get; set; }

        [Required()]
        [StringLength(25)]        
        public string Tamanho { get; set; }

    }

}