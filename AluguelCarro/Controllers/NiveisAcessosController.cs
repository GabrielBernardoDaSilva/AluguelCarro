using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AluguelCarro.Models;
using AluguelCarro.AcessoDados.Interfaces;
using Microsoft.Extensions.Logging;

namespace AluguelCarro.Controllers
{
    public class NiveisAcessosController : Controller
    {
        private readonly INivelAcessoRepositorio _nivelAcessoRepositorio;
        private readonly ILogger<NiveisAcessosController> _logger;

        public NiveisAcessosController(INivelAcessoRepositorio nivelAcessoRepositorio, ILogger<NiveisAcessosController> logger)
        {
            _nivelAcessoRepositorio = nivelAcessoRepositorio;
            _logger = logger;
        }





        // GET: NiveisAcessos
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando os Registros");
            return View(await _nivelAcessoRepositorio.PegarTodos().ToListAsync());
        }


        // GET: NiveisAcessos/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Iniciando criação de niveis de acesso");
            return View();
        }

        // POST: NiveisAcessos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descrircao,Name")] NiveisAcesso niveisAcesso)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Verificando o nivel de acesso");
                bool nivelExiste = await _nivelAcessoRepositorio.NivelAcessoExist(niveisAcesso.Name);
                if (!nivelExiste)
                {
                    niveisAcesso.NormalizedName = niveisAcesso.Name.ToUpper();
                    await _nivelAcessoRepositorio.Inserir(niveisAcesso);
                    _logger.LogInformation("Inseridos com sucesso!");


                    return RedirectToAction("Index", "NiveisAcessos");
                }
            }
            _logger.LogError("Informações invalidas");
            return View(niveisAcesso);
        }

        // GET: NiveisAcessos/Edit/5
        public async Task<IActionResult> Edit(string id)
        {

            _logger.LogInformation("Atualizando o nivel de acesso");
            if (id == null)
            {
                _logger.LogError("Informações invalidas");
                return NotFound();
            }

            var niveisAcesso = await _nivelAcessoRepositorio.PegarPeloId(id);
            if (niveisAcesso == null)
            {
                return NotFound();
            }
            return View(niveisAcesso);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Descrircao,Name")] NiveisAcesso niveisAcesso)
        {
            if (id != niveisAcesso.Id)
            {
                _logger.LogError("Informações invalidas");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _nivelAcessoRepositorio.Atualizar(niveisAcesso);
                _logger.LogInformation("Atualizado o nivel de acesso");
                return RedirectToAction(nameof(Index));
                
            }
            _logger.LogError("Informações invalidas");
            return View(niveisAcesso);
        }



        // POST: NiveisAcessos/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _nivelAcessoRepositorio.Excluir(id);
            _logger.LogInformation("Excluindo o nivel de acesso");
            return RedirectToAction(nameof(Index));
        }


    }
}
