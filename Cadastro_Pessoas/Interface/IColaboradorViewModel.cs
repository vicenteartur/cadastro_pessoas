using Cadastro_Pessoas.Models;
using Cadastro_Pessoas.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Interface
{
    interface IColaboradorViewModel
    {
        Task<TbColaborador> MontarAdmin(int id);
        Task<ColaboradorViewModel> MontarColaborador(int CodigoAdministrador, TbColaborador colaborador);
        Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, int CodigoCargo);
        Task<TbColaborador> localizaColaborador(int codigo);
        Task<List<ColaboradorViewModel>> ColaboradorAtivo(int CodigoAdministrador);
        Task<List<TbColaborador>> ColaboradorInativo(int CodigoAdministrador);
        Task InserirColaborador(ColaboradorViewModel colaborador);
        Task AtualizarColaborador(ColaboradorViewModel colaborador);
        Task AtivarColaborador(ColaboradorViewModel colaborador);
        Task InativarColaborador(ColaboradorViewModel colaborador);
        Task RemoverColaborador(ColaboradorViewModel colaborador);
        

    }
}