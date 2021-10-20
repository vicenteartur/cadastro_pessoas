using Cadastro_Pessoas.Interface;
using Cadastro_Pessoas.Models;
using Cadastro_Pessoas.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Controllers
{
    public class CargoController : Controller
    {
        private readonly ICargo _cargo;

        public CargoController()
        {
            _cargo = new CargoService();
        }

        // GET: Cargo
        public async Task<IActionResult> Index(int? id)
        {
            var admin = await _cargo.MontarAdmin((int)id);

            ViewData["admin"] = admin;

            return View(await _cargo.ListaCargo());
        }

        // GET: Cargo/Details/5
        public async Task<IActionResult> Details(int? id, int? cargo)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            if (id == null)
            {
                return NotFound();
            }

            var tbCargo = await _cargo.Detalhes((int)cargo);

            if (tbCargo == null)
            {
                return NotFound();
            }

            return View(tbCargo);
        }

        // GET: Cargo/Create
        public async Task<IActionResult> CreateAsync(int? id)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;
            return View();
        }

        // POST: Cargo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Cargo,NiveldeAcesso")] TbCargo tbCargo)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (ModelState.IsValid)
            {
                await _cargo.Inserir(tbCargo);

                return RedirectToAction("Index", new { id = id });
            }
            return View(tbCargo);
        }

        // GET: Cargo/Edit/5
        public async Task<IActionResult> Edit(int? id, int? cargo)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }
            var tbCargo = new TbCargo();
            tbCargo = await _cargo.Detalhes((int)cargo);

            if (tbCargo == null)
            {
                return NotFound();
            }
            return View(tbCargo);
        }

        // POST: Cargo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Cargo,NiveldeAcesso")] TbCargo tbCargo)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cargo.Atualizar(tbCargo);

                }
                catch (Exception)
                {
                    if (!_cargo.TbCargoExists(tbCargo.Codigo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = id });
            }
            return View(tbCargo);
        }

        // GET: Cargo/Delete/5
        public async Task<IActionResult> Delete(int? id, int? cargo)
        {
            var admin = await _cargo.MontarAdmin((int)id);
            ViewData["admin"] = admin;

            if (id == null)
            {
                return NotFound();
            }

            var tbCargo = await _cargo.Detalhes((int)cargo);
            if (tbCargo == null)
            {
                return NotFound();
            }

            return View(tbCargo);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? codigo)
        {
            var tbEstado = await _cargo.Detalhes((int)codigo);
            await _cargo.Deletar(tbEstado);

            return RedirectToAction("Index", new { id = id });
        }
    }
}