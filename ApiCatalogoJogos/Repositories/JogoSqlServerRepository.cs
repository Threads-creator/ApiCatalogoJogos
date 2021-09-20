﻿using ApiCatalogoJogos.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public class JogoSqlServerRepository : IJogoRepository
    {
        private readonly SqlConnection _sqlConnection;

        public JogoSqlServerRepository(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task Atualizar(Jogo jogo)
        {
            var comando = $"update Jogos set Nome = '{jogo.Nome}', Produtora = '{jogo.Produtora}', Preco =  {jogo.Preco} where Id = '{jogo.Id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await _sqlConnection.CloseAsync();

        }

        public void Dispose()
        {
            _sqlConnection?.Close();
            _sqlConnection?.Dispose();
        }

        public async Task Inserir(Jogo jogo)
        {
            var comando = $"insert into Jogos (Id, Nome, Produtora, Preco) values ('{jogo.Id}', '{jogo.Nome}', '{jogo.Produtora}', {jogo.Preco})";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await _sqlConnection.CloseAsync();

        }

        public async Task<List<Jogo>> Obter(int pagina, int quantidade)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos order by id offset {((pagina - 1) * quantidade)} rows fetch next {quantidade} rows only";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = Decimal.ToDouble((decimal) sqlDataReader["Preco"])
                });
            }

            await _sqlConnection.CloseAsync();
            return jogos;
        }

        public async Task<Jogo> Obter(Guid id)
        {
            Jogo jogo = null;

            var comando = $"select * from Jogos where Id = '{id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogo = new Jogo
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = Decimal.ToDouble((decimal)sqlDataReader["Preco"])
                };
            }

            await _sqlConnection.CloseAsync();
            return jogo;

        }

        public async Task<List<Jogo>> Obter(string nome, string produtora)
        {
            var jogos = new List<Jogo>();

            var comando = $"select * from Jogos where Nome = '{nome}' and Produtora = '{produtora}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                jogos.Add(new Jogo
                {
                    Id = Guid.Parse(sqlDataReader["Id"].ToString()),
                    Nome = (string)sqlDataReader["Nome"],
                    Produtora = (string)sqlDataReader["Produtora"],
                    Preco = Decimal.ToDouble((decimal)sqlDataReader["Preco"])
                });
            }

            await _sqlConnection.CloseAsync();
            return jogos;
        }

        public async Task Remover(Guid id)
        {
            var comando = $"delete from Jogos where Id = '{id}'";

            await _sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(comando, _sqlConnection);
            sqlCommand.ExecuteNonQuery();

            await _sqlConnection.CloseAsync();
        }
    }
}
