using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Cadastro_Pessoas.ViewModel
{
    public class ColaboradorViewModel
    {

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public int CodigoCargo { get; set; }
        public string Cargo { get; set; }
        public int NiveldeAcesso { get; set; }
        public byte Ativo { get; set; }
        public List<SelectListItem> cargo { get; set; }
        public int CodigoEscola { get; set; }
        public string NomeEscola { get; set; }
        public List<SelectListItem> escola { get; set; }
        public int CodigoAdministrador { get; set; }
        public string NomeAdministrador { get; set; }
        public int CodigoCargoAdministrador { get; set; }
        public string CargoAdministrador { get; set; }
        public int NiveldeAcessoAdministrador { get; set; }
    }
}