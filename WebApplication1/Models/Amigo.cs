using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Amigo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [InverseProperty("Amigo_Emprestimo")]
        public List<Emprestimo> Emprestimos { get; set; }
    }
}
