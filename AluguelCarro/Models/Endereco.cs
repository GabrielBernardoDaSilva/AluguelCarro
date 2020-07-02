using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models
{
    public class Endereco
    {
        public int EndercoId { get; set; }
        [Required(ErrorMessage ="Campo obrigatorio!")]
        [StringLength(100,ErrorMessage ="Use menos caracteres!")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [Range(0,int.MaxValue,ErrorMessage ="Valor invalido!")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(50, ErrorMessage = "Use menos caracteres!")]
        public string Estado { get; set; }

        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
