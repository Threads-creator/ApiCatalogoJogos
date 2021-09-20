using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoRepository : IJogoRepository
    {
        private static Dictionary<Guid, Jogo> jogos = new Dictionary<Guid, Jogo>()
        {
            { Guid.Parse("002d2452-332c-40bc-a6a4-4f5a6f98a570"),  new Jogo { Id = Guid.Parse("002d2452-332c-40bc-a6a4-4f5a6f98a570"), Nome = "God of War", Preco = 100.0, Produtora = "Santa Mônica"}},
            { Guid.Parse("cd2dca87-6c62-40b0-81c9-126f0968250b"),  new Jogo { Id = Guid.Parse("cd2dca87-6c62-40b0-81c9-126f0968250b"), Nome = "Call of Duty BO3", Preco = 200.0, Produtora = "Activision"}},
            { Guid.Parse("d03f5959-d48c-4da5-931f-671afb965ff6"),  new Jogo { Id = Guid.Parse("d03f5959-d48c-4da5-931f-671afb965ff6"), Nome = "Battlefield 5", Preco = 300.0, Produtora = "EA"}},
            { Guid.Parse("14af23a4-7bb0-4e77-a4cd-6eb633518e97"),  new Jogo { Id = Guid.Parse("14af23a4-7bb0-4e77-a4cd-6eb633518e97"), Nome = "The last of Us", Preco = 100.0, Produtora = "Santa Mônica"}},
            { Guid.Parse("59b15652-9e1e-4cae-b975-dc37331d0751"),  new Jogo { Id = Guid.Parse("59b15652-9e1e-4cae-b975-dc37331d0751"), Nome = "Call of Duty Ghosts", Preco = 200.0, Produtora = "Activision"}}
        };


        public Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            return Task.FromResult(jogos.Values.Skip((pagina - 1) * quantidade).Take(quantidade).ToList());
        }

        public Task<Jogo> Obter(Guid id)
        {
            if (!jogos.ContainsKey(id))
                return null;

            return Task.FromResult(jogos[id]);
        }
        public Task<List<Jogo>> Obter(string nome, string produtora)
        {
            return Task.FromResult(jogos.Values.Where(jogo => jogo.Nome.Equals(nome) && jogo.Produtora.Equals(produtora)).ToList());
        }
        public Task Inserir(Jogo jogo)
        {
            jogos.Add(jogo.Id, jogo);
            return Task.CompletedTask;
        }
        public Task Atualizar(Jogo jogo)
        {
            jogos[jogo.Id] = jogo;
            return Task.CompletedTask;

        }
        public Task Remover(Guid id)
        {
            jogos.Remove(id);
            return Task.CompletedTask;

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
