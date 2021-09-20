using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModels;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var jogoVerificado = await _jogoRepository.Obter(id);

            if (jogoVerificado == null)
                throw new JogoNaoCadastradoException();

            jogoVerificado.Nome = jogo.Nome;
            jogoVerificado.Preco = jogo.Preco;
            jogoVerificado.Produtora = jogo.Produtora;

            await _jogoRepository.Atualizar(jogoVerificado);

        }

        public async Task Atualizar(Guid id, double preco)
        {
            var jogoVerificado = await _jogoRepository.Obter(id);

            if (jogoVerificado == null)
                throw new JogoNaoCadastradoException();

            jogoVerificado.Preco = preco;
            
            await _jogoRepository.Atualizar(jogoVerificado);
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var jogoVerificado = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if (jogoVerificado.Count() > 0)
                throw new JogoJaCadastradoException();

            var jogoInserido = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };

            await _jogoRepository.Inserir(jogoInserido);

            return new JogoViewModel
            {
                IdJogo = jogoInserido.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };

        }

        public async Task<IEnumerable<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            if (jogos == null)
                return null;

            return jogos.Select(jogo => new JogoViewModel
            {
                IdJogo = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                IdJogo = jogo.Id,
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };
            
        }

        public async Task Remover(Guid id)
        {
            var JogoVerificado = await _jogoRepository.Obter(id);

            if (JogoVerificado == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
