using AutoMapper;
using Desafio.Data;
using Desafio.Helpers;
using Desafio.Models;
using Desafio.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Desafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TituloController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TituloController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// - Insere um novo título de dívida na base de dados.
        /// </summary>
        /// <param name="vm">Objeto TituloViewModel contendo os dados do título de dívida a ser inserido.</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        /// 
        /// {
        ///     "numeroTitulo": "111",
        ///     "nomeDevedor": "João Silva",
        ///     "cpfDevedor": "01234567890",
        ///     "percentualJuros": 1,
        ///     "percentualMulta": 2,
        ///     "parcelas": [
        ///         { "numeroParcela": 1, "dataVencimento": "2025-12-29", "valorParcela": 750.00},
        ///         { "numeroParcela": 2, "dataVencimento": "2025-12-29", "valorParcela": 750.00}
        ///     ]
        /// }
        /// </remarks>
        /// <returns>Retorna o título inserido com os dados salvos no banco.</returns>
        /// <response code="200">Título de dívida inserido com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar inserir o título de dívida.</response>
        [HttpPost, Route("Inserir")]
        public async Task<IActionResult> Inserir([FromBody] TituloViewModel vm)
        {
            try
            {
                var helper = new TituloHelper();
                var verificarCampos = helper.VerificarCampos(vm);

                if (!verificarCampos.IsNullOrEmpty())
                    return BadRequest("Erro: "+verificarCampos);

                var model = _mapper.Map<TituloModel>(vm);

                await _context.Titulos.AddAsync(model);
                await _context.SaveChangesAsync();

                return Ok(vm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// - Retorna títulos de dívida na base de dados.
        /// </summary>
        /// <param name="nomeDevedor">Valor opcional contendo o nome do devedor a ser filtrado.</param>
        /// <returns>Retorna os títulos de dívida na base de dados.</returns>
        /// <response code="200">Título(s) de dívida retornado(s) com sucesso.</response>
        /// <response code="400">Ocorreu um erro ao tentar retornar o título de dívida.</response>
        [HttpGet, Route("Listar")]
        public async Task<IActionResult> Listar([FromQuery] string? nomeDevedor)
        {
            try
            {
                var query = _context.Titulos.AsQueryable();

                if (!string.IsNullOrEmpty(nomeDevedor))
                {
                    var filtro = nomeDevedor.ToUpper();
                    query = query
                        .Where(t => t.NomeDevedor.ToUpper().Contains(filtro))
                        .Include(t => t.Parcelas);
                }
                else
                {
                    // Sem filtro
                    query = query
                        .Include(t => t.Parcelas);
                }
                var titulos = await query.ToListAsync();
                var vmList = _mapper.Map<List<TituloViewModel>>(titulos);

                return Ok(vmList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}