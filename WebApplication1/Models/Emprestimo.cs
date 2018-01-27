using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Amigo_EmprestimoId")]
        public Amigo Amigo_Emprestimo { get; set; }
        [ForeignKey("Jogo_EmprestimoId")]
        public Jogo Jogo_Emprestimo { get; set; }
        [ForeignKey("Amigo_EmprestimoId")]
        [Required]
        public int Amigo_EmprestimoId { get; set; }
        [ForeignKey("Jogo_EmprestimoId")]
        [Required]
        public int Jogo_EmprestimoId { get; set; }
        [Required]
        public DateTime DataHoraEmprestimo { get; set; }
    }
}
