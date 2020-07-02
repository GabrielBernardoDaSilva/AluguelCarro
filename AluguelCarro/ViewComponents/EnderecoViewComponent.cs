using AluguelCarro.AcessoDados.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluguelCarro.ViewComponents
{
    public class EnderecoViewComponent : ViewComponent
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IEnderecoRepositorio _enderecoRepositorio;

        public EnderecoViewComponent(IUsuarioRepositorio usuarioRepositorio, IEnderecoRepositorio enderecoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _enderecoRepositorio = enderecoRepositorio;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var usuario = await _usuarioRepositorio.PegarUsuarioLogado(HttpContext.User);
            var enderecos = _enderecoRepositorio.PegarTodos().Where(e => e.UsuarioId == usuario.Id);
            return View(await enderecos.ToListAsync());

        }
    }
}
