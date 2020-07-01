using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Models;
using FichaAcademia.AcessoDados.Repositorios;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Repositorios
{
    public class NivelAcessoRepositorio : RepositorioGenerico<NiveisAcesso>, INivelAcessoRepositorio
    {

        private readonly RoleManager<NiveisAcesso> _gerenciadoNiveisAcesso;

        public NivelAcessoRepositorio(RoleManager<NiveisAcesso> gerenciadoNiveisAcesso,Contexto contexto) : base(contexto)
        {
            _gerenciadoNiveisAcesso = gerenciadoNiveisAcesso;
        }

        public async Task<bool> NivelAcessoExist(string nivelAcesso)
        {
            return await _gerenciadoNiveisAcesso.RoleExistsAsync(nivelAcesso);
        }
    }
}
