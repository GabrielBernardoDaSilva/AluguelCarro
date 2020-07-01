using AluguelCarro.Models;
using FichaAcademia.AcessoDados.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AluguelCarro.AcessoDados.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioGenerico<Usuario>
    {
        Task<Usuario> PegarUsuarioLogado(ClaimsPrincipal usuario);
        Task<IdentityResult> SalvarUsuario(Usuario usuario,string senha);
        Task AtualizarUsuario(Usuario usuario);
        Task AtribuirNivelDeAcesso(Usuario usuario,string nivelDeAcesso);
        Task EfetuarLogin(Usuario usuario,bool lembrar);
        Task EfetuarLogout();
        Task<Usuario> PegarUsuarioPorEmai(string email);

    }
}
