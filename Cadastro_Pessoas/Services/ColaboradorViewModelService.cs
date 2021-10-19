using Cadastro_Pessoas.Interface;
using Cadastro_Pessoas.Models;
using Cadastro_Pessoas.ViewModel;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Services
{
    public class ColaboradorViewModelService : IColaboradorViewModel
    {

        private dbContext db = new dbContext();

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors.Include(c => c.CodigoCargoNavigation)
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }
        private bool TbColaboradorExists(string email)
        {
            return db.TbColaboradors.Any(e => e.Email == email);
        }

        public async Task<TbColaborador> localizaColaborador(int codigo)
        {
            var colaborador = await db.TbColaboradors.FindAsync(codigo);
            return colaborador;
        }

        public async Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, int CodigoCargo)
        {

            var colab = await (from col in db.TbColaboradors
                               join c in db.TbCargos on col.CodigoCargo equals c.Codigo
                               where col.Codigo == CodigoAdministrador
                               select c.NiveldeAcesso).FirstAsync();

            if (CodigoCargo != 0)
            {
                var lista = new List<SelectListItem>();
                var cargo = await db.TbCargos.Where(cg => cg.NiveldeAcesso <= colab).ToListAsync<TbCargo>();

                try
                {
                    foreach (var item in cargo)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Cargo,
                            Value = item.Codigo.ToString(),
                            Selected = (item.Codigo == CodigoCargo)
                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;

            }

            else
            {
                var lista = new List<SelectListItem>();
                var cargo = await db.TbCargos.Where(cg => cg.NiveldeAcesso <= colab).ToListAsync<TbCargo>();

                try
                {
                    foreach (var item in cargo)
                    {
                        var option = new SelectListItem()
                        {
                            Text = item.Cargo,
                            Value = item.Codigo.ToString(),

                        };

                        lista.Add(option);

                    }
                }
                catch
                {

                    throw;
                }

                return lista;
            }
        }
        
        public async Task<ColaboradorViewModel> MontarColaborador(int CodigoAdministrador, TbColaborador colaborador)
        {
            if (colaborador == null)
            {

                var cargo = await ListaCargos(CodigoAdministrador, 0);
                

                var consulta = await (from col in db.TbColaboradors
                                      join cg in db.TbCargos
                                      on col.CodigoCargo equals cg.Codigo
                                      select new ColaboradorViewModel
                                      {
                                          CodigoAdministrador = col.Codigo,
                                          NomeAdministrador = col.Nome,
                                          CodigoCargoAdministrador = Convert.ToInt32(col.CodigoCargo),
                                          CargoAdministrador = cg.Cargo,
                                          cargo = cargo
                                          
                                      }).FirstAsync();


                return consulta;
            }
            else
            {

                var cargo = await ListaCargos(CodigoAdministrador, (int)colaborador.CodigoCargo);
                

                var consulta = (from c in db.TbColaboradors
                                join cg in db.TbCargos
                                on c.CodigoCargo equals cg.Codigo
                                
                                select new ColaboradorViewModel
                                {
                                    Codigo = colaborador.Codigo,
                                    Nome = colaborador.Nome,
                                    Email = colaborador.Email,
                                    Ativo = colaborador.Ativo,
                                    CodigoCargo = Convert.ToInt32(colaborador.CodigoCargo),
                                    Cargo = colaborador.CodigoCargoNavigation.Cargo,
                                    NiveldeAcesso = colaborador.CodigoCargoNavigation.NiveldeAcesso
                                    
                                }).FirstAsync();


                return await consulta;
            }
        }

        public async Task<List<ColaboradorViewModel>> ColaboradorAtivo(int CodigoAdministrador, TbColaborador admin)
        {

           


            var consulta = await (from c in db.TbColaboradors
                                  join cg in db.TbCargos
                                  on c.CodigoCargo equals cg.Codigo
                                  where c.Ativo != 0 && cg.NiveldeAcesso <= admin.CodigoCargoNavigation.NiveldeAcesso
                                  select new ColaboradorViewModel { 
                                      Codigo = c.Codigo, 
                                      Nome = c.Nome, 
                                      Email = c.Email, 
                                      Cargo = cg.Cargo, 
                                      Ativo = c.Ativo, 
                                      CodigoAdministrador = CodigoAdministrador
                                      }).ToListAsync();

            return consulta;
        }

        public async Task<List<TbColaborador>> ColaboradorInativo(int CodigoAdministrador)
        {
            var admin = await (from ad in db.TbColaboradors
                               join cgad in db.TbCargos
                               on ad.CodigoCargo equals cgad.Codigo
                               where ad.Codigo == CodigoAdministrador
                               select cgad.NiveldeAcesso).FirstAsync();


            var consulta = await (from c in db.TbColaboradors
                                  join cg in db.TbCargos
                                  on c.CodigoCargo equals cg.Codigo
                                  where c.Ativo != 1 && cg.NiveldeAcesso <= admin
                                  select c).ToListAsync<TbColaborador>();

            return consulta;
        }

        public async Task InserirColaborador(ColaboradorViewModel colaborador)
        {

            if (!TbColaboradorExists(colaborador.Email))
            {
                var col = new TbColaborador()
                {
                    Nome = colaborador.Nome,
                    Email = colaborador.Email,
                    CodigoCargo = colaborador.CodigoCargo,
                    Senha = "1",
                    Ativo = 1
                };

                await db.TbColaboradors.AddAsync(col);
                await db.SaveChangesAsync();

                var colab = await db.TbColaboradors.Where(c => c.Email == col.Email).Include(cg => cg.CodigoCargoNavigation).FirstAsync();

            }   
        }

        public async Task AtualizarColaborador(ColaboradorViewModel colaborador)
        {
            colaborador.cargo = await ListaCargos(colaborador.CodigoAdministrador, colaborador.CodigoCargo);
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                Senha = "1",
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = colaborador.Ativo
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }

        public async Task RemoverColaborador(ColaboradorViewModel colaborador)
        {
            
            var remover_colab = await db.TbColaboradors.Where(c => c.Codigo == colaborador.Codigo).FirstAsync();

            db.TbColaboradors.Remove(remover_colab);

            await db.SaveChangesAsync();

        }

        public async Task AtivarColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = 1
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }

        public async Task InativarColaborador(ColaboradorViewModel colaborador)
        {
            var col = new TbColaborador()
            {
                Codigo = colaborador.Codigo,
                Nome = colaborador.Nome,
                Email = colaborador.Email,
                CodigoCargo = colaborador.CodigoCargo,
                Ativo = 0
            };

            db.TbColaboradors.Update(col);
            await db.SaveChangesAsync();
        }

        
    }
}
