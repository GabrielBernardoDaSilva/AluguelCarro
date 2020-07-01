using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace AluguelCarro.Models
{
    public class Usuario : IdentityUser
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public Conta Conta { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
