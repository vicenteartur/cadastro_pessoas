using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadastro_Pessoas.Models
{
    public partial class TbColaborador
    {
                
            public int Codigo { get; set; }
            public string Nome { get; set; }
            public string Email { get; set; }
            public string Senha { get; set; }
            public int CodigoCargo { get; set; }
            public byte Ativo { get; set; }

            public virtual TbCargo CodigoCargoNavigation { get; set; }
        
    }
}
