using Cadastro_Pessoas.Interface;
using Cadastro_Pessoas.Services;
using Cadastro_Pessoas.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcompanhamentoDocente.Controllers
{
    public class ColaboradorViewController : Controller

    {

        private readonly IColaboradorViewModel _colabview;

        public ColaboradorViewController()
        {
            _colabview = new ColaboradorViewModelService();
        }


        // GET: ColaboradorViewController
        public async Task<ActionResult> Index(int id)
        {
            var model = new List<ColaboradorViewModel>();
            model = await _colabview.ColaboradorAtivo(id);
            var col = await _colabview.MontarAdmin(id);
            ViewData["admin"] = col;
            return View(model);
        }

        // GET: ColaboradorViewController/Details/5
        public async Task<ActionResult> Details(int id, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            ViewData["admin"] = await _colabview.MontarAdmin(id);
            
            return View(await _colabview.MontarColaborador(id, colaborador));
        }

        // GET: ColaboradorViewController/Create
        public async Task<ActionResult> Create(int id, int esc)
        {
            ViewData["admin"] = await _colabview.MontarAdmin(id);
            
            return View(await _colabview.MontarColaborador(id, esc, null));
        }

        // POST: ColaboradorViewController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo,CodigoEscola,CodigoAdministrador")] ColaboradorViewModel colaborador)
        {
            int esc = colaborador.CodigoEscola;

            if (ModelState.IsValid)
            {

                try
                {
                    await _colabview.InserirColaborador(colaborador);
                    return RedirectToAction("Index", new { id = id, esc });

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                ViewData["admin"] = await _colabview.MontarAdmin((int)id);
                
                return RedirectToAction("Index", new { id = id});
            }

            var colab = await _colabview.MontarColaborador((int)id, null);
            return View(colaborador);
        }

        // GET: ColaboradorViewController/Edit/5
        public async Task<ActionResult> Edit(int id, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            ViewData["admin"] = await _colabview.MontarAdmin(id);
            
            return View(await _colabview.MontarColaborador(id, colaborador));
        }

        // POST: ColaboradorViewController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo")] ColaboradorViewModel colaborador)
        {

            

            if (id != colaborador.CodigoAdministrador)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    ViewData["admin"] = await _colabview.MontarAdmin((int)id);
                    
                    await _colabview.AtualizarColaborador(colaborador);
                    return RedirectToAction("Index", new { id = id});

                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction("Index", new { id = id});
            }
            var col = await _colabview.localizaColaborador(colaborador.Codigo);
            var colab = await _colabview.MontarColaborador((int)id, col);
            ViewData["admin"] = await _colabview.MontarAdmin((int)id);
            
            return View(colaborador);

        }

        // GET: ColaboradorViewController/Delete/5
        public async Task<IActionResult> Delete(int id, int esc, int col)
        {
            var colaborador = await _colabview.localizaColaborador(col);
            ViewData["admin"] = await _colabview.MontarAdmin(id);
            
            return View(await _colabview.MontarColaborador(id, esc, colaborador));
        }

        // POST: ColaboradorViewController/Delete/5
        [HttpPost]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([Bind("Codigo,Nome,Email,Ativo,CodigoCargo,Cargo,CodigoEscola,CodigoAdministrador")] ColaboradorViewModel colaborador)
        {
            try
            {

                var removercolab = await _colabview.localizaColaborador(colaborador.Codigo);
                var montarremovido = await _colabview.MontarColaborador(colaborador.CodigoAdministrador, colaborador.CodigoEscola, removercolab);
                await _colabview.RemoverColaborador(montarremovido);

                return RedirectToAction("Index", new { id = colaborador.CodigoAdministrador, esc = colaborador.CodigoEscola });
            }
            catch (Exception ex)
            {
                ViewData["admin"] = await _colabview.MontarAdmin(colaborador.CodigoAdministrador);
                
                return View(colaborador);
            }
        }

        
    }
}