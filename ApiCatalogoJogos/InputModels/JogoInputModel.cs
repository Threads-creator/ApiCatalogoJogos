using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.InputModels
{
    public class JogoInputModel
    {
        [Required, StringLength(100, MinimumLength = 3, ErrorMessage = "Campo Obrigatório, com 3 a 100 caracteres")]
        public string Nome { get; set; }
        [Required, StringLength(100, MinimumLength = 1, ErrorMessage = "Campo Obrigatório, com 1 a 100 caracteres")]
        public string Produtora { get; set; }
        [Required, Range(1, 1000, ErrorMessage = "Campo Obrigatório, com valor de 1 a 1000 reais")]
        public double Preco { get; set; }
    }
}
