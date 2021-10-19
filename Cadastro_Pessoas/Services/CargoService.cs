using Cadastro_Pessoas.Interface;
using Cadastro_Pessoas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Services
{
    public class CargoService : ICargo
    {
        private dbContext db = new dbContext();

        public async Task Atualizar(TbCargo cargo)
        {
            db.TbCargos.Update(cargo);
            await db.SaveChangesAsync();
        }

        public async Task Deletar(TbCargo cargo)
        {
            db.TbCargos.Remove(cargo);
            await db.SaveChangesAsync();
        }

        public async Task<TbCargo> Detalhes(int id)
        {
            var tbcargo = await db.TbCargos.Where(e => e.Codigo == id).FirstAsync();
            return tbcargo;
        }

        public async Task Inserir(TbCargo cargo)
        {
            await db.TbCargos.AddAsync(cargo);
            await db.SaveChangesAsync();
        }

        public async Task<List<TbCargo>> ListaCargo()
        {
            return await db.TbCargos.OrderBy(c => c.Cargo).ToListAsync();
        }

        public async Task<TbColaborador> MontarAdmin(int id)
        {
            var tbcolaborador = await db.TbColaboradors
                .FirstOrDefaultAsync(m => m.Codigo == id);

            return tbcolaborador;
        }

        public bool TbCargoExists(int id)
        {
            return db.TbCargos.Any(e => e.Codigo == id);
        }
    }
}