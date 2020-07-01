using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.Models
{
    public class Carro
    {
        public int CarroId { get; set; }
        public string Nome { get; set; }
        public int Marca { get; set; }
        public string Foto { get; set; }

        public double Diaria { get; set; }
        public ICollection<Aluguel> Alugueis { get; set; }
    }
}
