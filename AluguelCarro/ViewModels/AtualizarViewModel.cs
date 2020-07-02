using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewModels
{
    public class AtualizarViewModel
    {

        public string UsuarioId { get; set; }
        [Required(ErrorMessage ="Campo obrigatorio!")]
        [StringLength(100,ErrorMessage ="Use menos caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatorio!")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Campo obrigatorio!")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(100, ErrorMessage = "Use menos caracteres")]
        public string NomeUsuario { get; set; }
        [Required(ErrorMessage = "Campo obrigatorio!")]
        [StringLength(100, ErrorMessage = "Use menos caracteres")]
        [DataType(DataType.EmailAddress,ErrorMessage ="Email ivalido!")]
        public string Email { get; set; }
    }
}
