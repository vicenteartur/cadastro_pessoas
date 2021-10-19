using Cadastro_Pessoas.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Interface
{
    interface ICargo
    {

        Task<List<TbCargo>> ListaCargo();
        Task<TbCargo> Detalhes(int id);
        Task Inserir(TbCargo cargo);
        Task Atualizar(TbCargo cargo);
        Task Deletar(TbCargo cargo);

        Task<TbColaborador> MontarAdmin(int id);
        bool TbCargoExists(int id);

    }
}
