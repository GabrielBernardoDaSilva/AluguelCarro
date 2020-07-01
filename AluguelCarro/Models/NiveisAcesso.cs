using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models
{
    public class NiveisAcesso : IdentityRole
    {
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage ="Campo Obrigatorio!")]
        [MaxLength(400,ErrorMessage ="Utilize menos caracteres!")]
        [DisplayName("Descrição")]
        public string Descrircao { get; set; }
    }
}
