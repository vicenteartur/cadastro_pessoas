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
        Task<List<SelectListItem>> ListaEscolas(int CodigoAdministrador, int CodigoEscola);
        Task<List<SelectListItem>> ListaCargos(int CodigoAdministrador, int CodigoCargo);
        Task<TbColaborador> localizaColaborador(int codigo);
        Task<ColaboradorViewModel> MontarColaborador(int CodigoAdministrador, int CodigoEscola, TbColaborador colaborador);
        Task<List<ColaboradorViewModel>> ColaboradorAtivo(int CodigoAdministrador, int CodigoEscola);
        Task<List<TbColaborador>> ColaboradorInativo(int CodigoAdministrador, int CodigoEscola);
        Task<List<ColaboradorViewModel>> EstenderJornada(int CodigoAdministrador, int CodigoEscola, string email);
        Task InserirColaborador(ColaboradorViewModel colaborador);
        Task AtualizarColaborador(ColaboradorViewModel colaborador);
        Task AtivarColaborador(ColaboradorViewModel colaborador);
        Task InativarColaborador(ColaboradorViewModel colaborador);
        Task RemoverColaborador(ColaboradorViewModel colaborador);
        Task AtribuirColaboradorEstendido(int Colaborador, int Escola);

    }
}