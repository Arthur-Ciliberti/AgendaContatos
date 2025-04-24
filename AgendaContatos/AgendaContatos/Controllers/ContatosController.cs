using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgendaContatos.Data;
using AgendaContatos.Models;
using AgendaContatos.DTOs;

namespace AgendaContatos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatosController(AgendaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retorna todos os contatos cadastrados no Banco de Dados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contato>>> GetContatos()
        {
            return await _context.Contatos.ToListAsync();
        }

        /// <summary>
        /// Retorna um contato específico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Contato>> GetContato(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null) return NotFound();
            
            return contato;
        }

        /// <summary>
        /// Cria um novo contato.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Contato>> PostContato(ContatoCreateDTO dto)
        {
            var contato = new Contato
            {
                Nome = dto.Nome,
                Apelido = dto.Apelido,
                CPF = dto.CPF,
                Telefone = dto.Telefone,
                Email = dto.Email,
                DataCadastro = DateTime.Now,
                DataUltimaAlteracao = DateTime.Now
            };

            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContato), new { id = contato.Id }, contato);
        }

        /// <summary>
        /// Atualiza um contato existente.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, ContatoCreateDTO dto)
        {
            var contatoExistente = await _context.Contatos.FindAsync(id);
            if (contatoExistente == null) return NotFound();

            contatoExistente.Nome = dto.Nome;
            contatoExistente.Apelido = dto.Apelido;
            contatoExistente.CPF = dto.CPF;
            contatoExistente.Telefone = dto.Telefone;
            contatoExistente.Email = dto.Email;
            contatoExistente.DataUltimaAlteracao = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Deleta um contato pelo ID e o move para a tabela de backup.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var contato = await _context.Contatos.FindAsync(id);
            if (contato == null) return NotFound();

            await _context.Database.ExecuteSqlRawAsync("CALL mover_para_backup({0})", id);

            return NoContent();
        }

        /// <summary>
        /// Lista todos os contatos que foram excluidos e movidos para a tabela de backup.
        /// </summary>
        [HttpGet("backup")]
        public async Task<IActionResult> ListarBackups()
        {
            var backups = await _context.Contatos
                .FromSqlRaw("SELECT * FROM contato_backup")
                .ToListAsync();

            return Ok(backups);
        }
    }
}
