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
    public class ColaboradorController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ColaboradorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Colaborador
        /// <summary>
        /// Busca os dados já existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codCol": 0, --Código do colaborador
        ///    "nome": "string", --Nome do colaborador
        ///    "ctps": "string", --Ctps do colaborador
        ///    "dtAdm": "2024-04-25", --Data de adimição do colaborador
        ///    "tel": 0, --Telefone do colaborador
        ///    "cpf": 0, --Cpf do colaborador
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Colaborador>>> GetColaboradors()
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            return await _context.Colaboradors.ToListAsync();
        }
        
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codCol": 0, --Código do colaborador
        ///    "nome": "string", --Nome do colaborador
        ///    "ctps": "string", --Ctps do colaborador
        ///    "dtAdm": "2024-04-25", --Data de adimição do colaborador
        ///    "tel": 0, --Telefone do colaborador
        ///    "cpf": 0, --Cpf do colaborador
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Colaborador/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Colaborador>> GetColaborador(int id)
        {
          if (_context.Colaboradors == null)
          {
              return NotFound();
          }
            var colaborador = await _context.Colaboradors.FindAsync(id);

            if (colaborador == null)
            {
                return NotFound();
            }

            return colaborador;
        }

        // PUT: api/Colaborador/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Altera os dados para o Id informado, conforme dados passados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put do cadastro, vc deve imformar os valores comforme no exemplo abaixo:
        ///    {
        ///    "codCol": 0, --Código do colaborador
        ///    "nome": "string", --Nome do colaborador
        ///    "ctps": "string", --Ctps do colaborador
        ///    "dtAdm": "2024-04-25", --Data de adimição do colaborador
        ///    "tel": 0, --Telefone do colaborador
        ///    "cpf": 0, --Cpf do colaborador
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <param name="colaborador"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaborador(int id, Colaborador colaborador)
        {
            if (id != colaborador.CodCol)
            {
                return BadRequest();
            }

            _context.Entry(colaborador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradorExists(id))
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

        // POST: api/Colaborador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Postagem do cadastro/insere os dados(cadastro)
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Post do cadastro os dados retornados serão
        ///    {
        ///    "codCol": 0, --Código do colaborador
        ///    "nome": "string", --Nome do colaborador
        ///    "ctps": "string", --Ctps do colaborador
        ///    "dtAdm": "2024-04-25", --Data de adimição do colaborador
        ///    "tel": 0, --Telefone do colaborador
        ///    "cpf": 0, --Cpf do colaborador
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="colaborador"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Colaborador>> PostColaborador(Colaborador colaborador)
        {
          if (_context.Colaboradors == null)
          {
              return Problem("Entity set 'AppDbContext.Colaboradors'  is null.");
          }
            _context.Colaboradors.Add(colaborador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColaborador", new { id = colaborador.CodCol }, colaborador);
        }

        // DELETE: api/Colaborador/5
        /// <summary>
        /// Deletar dados do cadastro conforme Id informado
        /// </summary>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColaborador(int id)
        {
            if (_context.Colaboradors == null)
            {
                return NotFound();
            }
            var colaborador = await _context.Colaboradors.FindAsync(id);
            if (colaborador == null)
            {
                return NotFound();
            }

            _context.Colaboradors.Remove(colaborador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColaboradorExists(int id)
        {
            return (_context.Colaboradors?.Any(e => e.CodCol == id)).GetValueOrDefault();
        }
    }
}
