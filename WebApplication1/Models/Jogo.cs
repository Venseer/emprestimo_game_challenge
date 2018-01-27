using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Jogo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String Nome { get; set; }
        [NotMapped]
        public bool IsEmprestado{ get; set; }
        [NotMapped]
        public Emprestimo Emprestimo_Atual { get; set; }
    }
}
