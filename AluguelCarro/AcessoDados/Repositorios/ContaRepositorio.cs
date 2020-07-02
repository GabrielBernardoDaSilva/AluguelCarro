using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Models;
using FichaAcademia.AcessoDados.Repositorios;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositorios
{
    public class ContaRepositorio : RepositorioGenerico<Conta>, IContaRepositorio
    {


        public ContaRepositorio(Contexto contexto) : base(contexto)
        {

        }

        public double PegarSaldoPeloId(string id)
        {
            return _contexto.Contas.FirstOrDefault(c => c.UsuarioId == id).Saldo;
        }

        public new async Task<IEnumerable<Conta>> PegarTodos()
        {
            return await _contexto.Contas.Include(c => c.Usuario).ToListAsync();
        }
    }
}
