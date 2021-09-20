using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Entities
{
    public class Jogo
    {
        // igual da view mas poderia ser qualquer outro model. Exemplo modelo novo a partir de junção de tabelas diferentes
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Preco { get; set; }
    }
}
