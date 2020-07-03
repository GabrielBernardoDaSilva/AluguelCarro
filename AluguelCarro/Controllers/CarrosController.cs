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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AluguelCarro.Controllers
{
    public class CarrosController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ICarroRepositorio _carroRepositorio;
        private readonly ILogger<CarrosController> _logger;

        public CarrosController(IHostingEnvironment hostingEnvironment, ICarroRepositorio carroRepositorio, ILogger<CarrosController> logger)
        {
            _hostingEnvironment = hostingEnvironment;
            _carroRepositorio = carroRepositorio;
            _logger = logger;
        }





        // GET: Carros
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Listando todos os carros");
            return View(await _carroRepositorio.PegarTodos().ToListAsync());
        }

 

        // GET: Carros/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Novo carro");
            return View();
        }

        // POST: Carros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarroId,Nome,Marca,Foto,Diaria")] Carro carro,IFormFile arquivo)
        {
            if (ModelState.IsValid)
            {
                
                if(arquivo != null)
                {
                    _logger.LogInformation("Criando link para imagem");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("copiando arquivo para pasta");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("arquivo copiado");
                        carro.Foto = $"~/Imagens/{arquivo.FileName}";
                    }
                }
                _logger.LogInformation("Salvando novo carro");
                await _carroRepositorio.Inserir(carro);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogError("Dados ivalidos");
            return View(carro);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("atualizar carro");


            var carro = await _carroRepositorio.PegarPeloId(id);
            if (carro == null)
            {
                _logger.LogError("Carro não encontrado");
                return NotFound();
            }
            TempData["FotoCarro"] = carro.Foto;
            return View(carro);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarroId,Nome,Marca,Foto,Diaria")] Carro carro, IFormFile arquivo)
        {
            if (id != carro.CarroId)
            {
                _logger.LogError("Carro não encontrado"); 
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (arquivo != null)
                {
                    _logger.LogInformation("Criando link para imagem");
                    var linkUpload = Path.Combine(_hostingEnvironment.WebRootPath, "Imagens");
                    using (FileStream fileStream = new FileStream(Path.Combine(linkUpload, arquivo.FileName), FileMode.Create))
                    {
                        _logger.LogInformation("copiando arquivo para pasta");
                        await arquivo.CopyToAsync(fileStream);
                        _logger.LogInformation("arquivo copiado");
                        carro.Foto = $"~/Imagens/{arquivo.FileName}";
                    }
                } else
                    carro.Foto = TempData["FotoCarro"].ToString();

                _logger.LogInformation("Atualizando carro");
                await _carroRepositorio.Atualizar(carro);
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }


        // POST: Carros/Delete/5
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {

            var carro = await _carroRepositorio.PegarPeloId(id);

            string fotoCarro = carro.Foto;
            fotoCarro = fotoCarro.Replace("~", "wwwroot");
            System.IO.File.Delete(fotoCarro);

            await _carroRepositorio.Excluir(id);
            return Json("Carro excluido");
        }

     
    }
}
