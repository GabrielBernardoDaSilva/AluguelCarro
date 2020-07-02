using AluguelCarro.Models;
using FichaAcademia.AcessoDados.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Interfaces
{
    public interface IContaRepositorio : IRepositorioGenerico<Conta>
    {
        new Task<IEnumerable<Conta>> PegarTodos();
        double PegarSaldoPeloId(string id);
    }
}
