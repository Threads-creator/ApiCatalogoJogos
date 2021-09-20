using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModels;
using ApiCatalogoJogos.Services;
using ApiCatalogoJogos.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class JogosController : ControllerBase
    {
        private readonly IJogoService _jogoService;

        public JogosController(IJogoService jogoService)
        {
            _jogoService = jogoService;
        }

        /// <summary>
        ///  Busca todos os jogos de forma paginada
        /// </summary>
        /// <param name="pagina">Quantidade de páginas, Min = 1</param>
        /// <param name="quantidade">Quantidade de items por página, Min = 1, Max = 50</param>
        /// <returns>Lista de Jogos</returns>
        /// <response code="200"> Retorna a lista de jogos </response>
        /// <response code="204"> Caso nao haja jogos </response>
        [HttpGet]
        public async Task<ActionResult<List<JogoViewModel>>> FindAll([FromQuery, Range(1, int.MaxValue)] int pagina, [FromQuery, Range(1, int.MaxValue)] int quantidade)
        {
            var resultado = await _jogoService.Obter(pagina, quantidade);
            
            if (resultado.Count() == 0)
                return NoContent();
            return Ok(resultado);
        }

        /// <summary>
        ///  Busca um unico jogo
        /// </summary>
        /// <param name="idJogo">Identificado unico do jogo, tipo Guid</param>
        /// <returns> Retorna Jogo com id definido</returns>
        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> FindOne([FromRoute] Guid idJogo)
        {
            var resultado = await _jogoService.Obter(idJogo);

            if (resultado == null)
                return NoContent();
            return Ok(resultado);
        }

        /// <summary>
        ///  Insere um novo jogo no banco
        /// </summary>
        /// <param name="jogo">Novo jogo, tipo Jogo</param>
        /// <returns> Retorna jogo com novo Id criado </returns>
        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InsertJogo([FromBody] JogoInputModel jogo)
        {
            try
            {
                var resultado = await _jogoService.Inserir(jogo);
                return Ok(resultado);

            }catch(JogoJaCadastradoException e)
            {
                return UnprocessableEntity("Jogo ja existe para esta produtora");
            }
        }

        /// <summary>
        ///  Atualiza um jogo em sua totalidade
        /// </summary>
        /// <param name="idJogo">Identificador do jogo, tipo Guid</param>
        /// <param name="jogo">Jogo alterado, tipo Jogo</param>
        /// <returns> Status da alteração do jogo </returns>
        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> UpdateJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogo)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, jogo);
                return Ok();

            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Jogo não existe");
            }
        }

        /// <summary>
        ///  Atualiza apenas a propriedade preço do jogo
        /// </summary>
        /// <param name="idJogo">Identificador do jogo, tipo Guid</param>
        /// <param name="preco">Atributo preco do jogo, tipo double, Min = 1, Max = 1000</param>
        /// <returns>  Status da alteração do jogo </returns>
        [HttpPatch("{idJogo:guid}/preco/{preco:double}")]
        public async Task<ActionResult> UpdatePrecoJogo([FromRoute] Guid idJogo, [FromRoute] double preco)
        {
            try
            {
                await _jogoService.Atualizar(idJogo, preco);
                return Ok();

            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Jogo não existe");
            }
        }

        /// <summary>
        ///  Remove um jogo do banco
        /// </summary>
        /// <param name="idJogo">Identificador do jogo, tipo Guid</param>
        /// <returns> Status da alteração do jogo </returns>
        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeleteJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoService.Remover(idJogo);
                return Ok();

            }
            catch (JogoNaoCadastradoException e)
            {
                return NotFound("Jogo não existe");
            }
        }
    }
}
