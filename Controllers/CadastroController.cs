using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIprojeto.Context;
using APIprojeto.Models;

namespace APIprojeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Cadastro
        /// <summary>
        /// Busca os dados já existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codEpi": 0, --código do EPI
        ///    "nome": "string", --nome do EPI
        ///    "usuEpi": "string", --descrição de como usar o EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cadastro>>> GetCadastros()
        {
          if (_context.Cadastros == null)
          {
              return NotFound();
          }
            return await _context.Cadastros.ToListAsync();
        }

        // GET: api/Cadastro/5
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codEpi": 0, --código do EPI
        ///    "nome": "string", --nome do EPI
        ///    "usuEpi": "string", --descrição de como usar o EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Cadastro>> GetCadastro(int id)
        {
          if (_context.Cadastros == null)
          {
              return NotFound();
          }
            var cadastro = await _context.Cadastros.FindAsync(id);

            if (cadastro == null)
            {
                return NotFound();
            }

            return cadastro;
        }

        // PUT: api/Cadastro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Altera os dados para o Id informado, conforme dados passados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put do cadastro, vc deve imformar os valores comforme no exemplo abaixo:
        ///    {
        ///    "codEpi": 0, --código do EPI
        ///    "nome": "string", --nome do EPI
        ///    "usuEpi": "string", --descrição de como usar o EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCadastro(int id, Cadastro cadastro)
        {
            if (id != cadastro.CodEpi)
            {
                return BadRequest();
            }

            _context.Entry(cadastro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CadastroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cadastro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Postagem do cadastro/insere os dados(cadastro)
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Post do cadastro os dados retornados serão
        ///    {
        ///    "codEpi": 0, --código do EPI
        ///    "nome": "string", --nome do EPI
        ///    "usuEpi": "string", --descrição de como usar o EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="cadastro"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Cadastro>> PostCadastro(Cadastro cadastro)
        {
          if (_context.Cadastros == null)
          {
              return Problem("Entity set 'AppDbContext.Cadastros'  is null.");
          }
            _context.Cadastros.Add(cadastro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCadastro", new { id = cadastro.CodEpi }, cadastro);
        }

        // DELETE: api/Cadastro/5
        /// <summary>
        /// Deletar dados do cadastro conforme ID informado
        /// </summary>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCadastro(int id)
        {
            if (_context.Cadastros == null)
            {
                return NotFound();
            }
            var cadastro = await _context.Cadastros.FindAsync(id);
            if (cadastro == null)
            {
                return NotFound();
            }

            _context.Cadastros.Remove(cadastro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CadastroExists(int id)
        {
            return (_context.Cadastros?.Any(e => e.CodEpi == id)).GetValueOrDefault();
        }
    }
}
