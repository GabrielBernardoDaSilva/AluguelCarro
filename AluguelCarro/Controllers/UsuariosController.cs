using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AluguelCarro.AcessoDados.Interfaces;
using AluguelCarro.Models;
using AluguelCarro.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AluguelCarro.Controllers
{
    public class UsuariosController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio, ILogger<UsuariosController> logger)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _logger = logger;
        }

        public async Task<IActionResult> Registro()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.EfetuarLogout();

            _logger.LogInformation("Entrado na pagina de registro!");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {
            if (ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = registro.NomeUsuario,
                    Nome = registro.Nome,
                    Email = registro.Email,
                    CPF = registro.CPF,
                    Telefone = registro.Telefone,
                    PasswordHash = registro.Senha
                };
                _logger.LogInformation("Criando usuario");

                IdentityResult res = await _usuarioRepositorio.SalvarUsuario(usuario, registro.Senha);
                if(res.Succeeded)
                {
                    _logger.LogInformation("Usuario criado");
                    _logger.LogInformation("Usuario criado");
                    var nivelAcesso = "Cliente";

                    await _usuarioRepositorio.AtribuirNivelDeAcesso(usuario, nivelAcesso);
                    _logger.LogInformation("Atribuido com sucesso");

                    _logger.LogInformation("Logando usuario");

                    await _usuarioRepositorio.EfetuarLogin(usuario, false);

                    return RedirectToAction("Index", "Home");

                }
                _logger.LogError("Erro ao criar o usuario");

                foreach(var erro in res.Errors)
                {
                    ModelState.AddModelError("",erro.Description.ToString());
                }

            }
            _logger.LogError("Informações invalidas");
            return View(registro);
        }

        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                await _usuarioRepositorio.EfetuarLogout();

            _logger.LogInformation("Entrando no login");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("verificando usuario por email");
                var usuario = await _usuarioRepositorio.PegarUsuarioPorEmai(login.Email);
                if (usuario is null)
                {
                    ModelState.AddModelError("", "Email invalido!");
                    return View(login);
                }


                PasswordHasher<Usuario> passwordHasher = new PasswordHasher<Usuario>();

                if(passwordHasher.VerifyHashedPassword(usuario,usuario.PasswordHash,login.Senha) != PasswordVerificationResult.Failed)
                {
                    _logger.LogInformation("Informaçoes correta");
                    await _usuarioRepositorio.EfetuarLogin(usuario, false); ;
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Senha errada!");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout()
        {
            await _usuarioRepositorio.EfetuarLogout();
            return View("Login");
        }
    }
}
