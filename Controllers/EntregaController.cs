using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIprojeto.Context;
using APIprojeto.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIprojeto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class EntregaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EntregaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Entrega
        /// <summary>
        /// Busca os dados já existentes
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codEntrega": 0, --Código da entrega
        ///    "codCol": 0, --código do colaborador
        ///    "codEpi": 0, --códico do EPI
        ///    "dtVal": "2024-04-25", --data de validade do EPI
        ///    "dtEntrega": "2024-04-25", --Data de entrega do EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entrega>>> GetEntregas()
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            return await _context.Entregas.ToListAsync();
        }

        // GET: api/Entrega/5
        /// <summary>
        /// Alteração conforme dados passados para o id informado
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Get do cadastro os dados retornados serão:
        ///    {
        ///    "codEntrega": 0, --Código da entrega
        ///    "codCol": 0, --código do colaborador
        ///    "codEpi": 0, --códico do EPI
        ///    "dtVal": "2024-04-25", --data de validade do EPI
        ///    "dtEntrega": "2024-04-25", --Data de entrega do EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Entrega>> GetEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);

            if (entrega == null)
            {
                return NotFound();
            }

            return entrega;
        }

        // PUT: api/Entrega/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Altera os dados para o Id informado, conforme dados passados
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///    Put do cadastro, vc deve imformar os valores comforme no exemplo abaixo:
        ///    {
        ///    "codEntrega": 0, --Código da entrega
        ///    "codCol": 0, --código do colaborador
        ///    "codEpi": 0, --códico do EPI
        ///    "dtVal": "2024-04-25", --data de validade do EPI
        ///    "dtEntrega": "2024-04-25", --Data de entrega do EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <param name="entrega"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEntrega(int id, Entrega entrega)
        {
            if (id != entrega.CodEntrega)
            {
                return BadRequest();
            }

            _context.Entry(entrega).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntregaExists(id))
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

        // POST: api/Entrega
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Postagem do cadastro/insere os dados(cadastro)
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        /// 
        ///    Post do cadastro os dados retornados serão
        ///    {
        ///    "codEntrega": 0, --Código da entrega
        ///    "codCol": 0, --código do colaborador
        ///    "codEpi": 0, --códico do EPI
        ///    "dtVal": "2024-04-25", --data de validade do EPI
        ///    "dtEntrega": "2024-04-25", --Data de entrega do EPI
        ///    }
        /// </remarks>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="entrega"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Entrega>> PostEntrega(Entrega entrega)
        {
            if (_context.Entregas == null)
            {
                return Problem("Entity set 'AppDbContext.Entregas'  is null.");
            }
            _context.Entregas.Add(entrega);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEntrega", new { id = entrega.CodEntrega }, entrega);
        }

        // DELETE: api/Entrega/5
        /// <summary>
        /// Deletar dados do cadastro conforme Id informado
        /// </summary>
        /// <response code="200"> Sucesso no retorno dos dados</response>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntrega(int id)
        {
            if (_context.Entregas == null)
            {
                return NotFound();
            }
            var entrega = await _context.Entregas.FindAsync(id);
            if (entrega == null)
            {
                return NotFound();
            }

            _context.Entregas.Remove(entrega);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EntregaExists(int id)
        {
            return (_context.Entregas?.Any(e => e.CodEntrega == id)).GetValueOrDefault();
        }
    }
}
