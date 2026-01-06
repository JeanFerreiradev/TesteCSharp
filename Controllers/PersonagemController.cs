using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteCSharp.Data;
using TesteCSharp.Models;

namespace TesteCSharp.Controllers
{
    // Define que a classe é um controller
    [ApiController]
    // Define a rota principal do controller
    [Route("api/[controller]")]

    // Classe herda funcionalidades de um controlador base
    public class PersonagemController : ControllerBase
    {
        // Cria instancia da classe que faz conexão com o banco
        private readonly AppDbContext _appDbContext;

        // Construtor do controller
        public PersonagemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // Define rota do endpoint
        [HttpPost("add")]
        // Função assíncrona pois necessita aguardar resposta do banco
        // Espera objeto Personagem no corpo da requisição
        public async Task<IActionResult> AddPersonagem([FromBody] Personagem personagem)
        {
            // Verifica se o que foi enviado na requisição condiz com o que é solicitado no endpoint
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Adiciona o personagem no banco
            _appDbContext.PERSONAGEM.Add(personagem);
            await _appDbContext.SaveChangesAsync();

            return Created("Persongem criado com sucesso!", personagem);
        }

        // Busca todos os personagens
        [HttpGet("getAll")]
        public async Task<ActionResult<IEnumerable<Personagem>>> GetAllPersonagens()
        {
            // Transforma os dados da tabela em uma lista
            var personagens = await _appDbContext.PERSONAGEM.ToListAsync();

            return Ok(personagens);
        }

        // Busca um personagem pelo id
        // Espera id na url
        [HttpGet("getById/{id}")]
        public async Task<ActionResult<Personagem>> GetPersonagem(int id)
        {
            // Itera sobre o resultado em busca do id passado como parâmetro
            var personagem = await _appDbContext.PERSONAGEM.FindAsync(id);

            if (personagem == null)
            {
                return NotFound("Personagem não encontrado!");
            }

            return Ok(personagem);
        }

        // Atualiza o personagem de acordo com o id
        [HttpPut("updateById")]
        // Espera objeto Personagem no corpo da requisição
        public async Task<IActionResult> UpdatePersonagem([FromBody] Personagem personagemAtualizado)
        {
            // Verifica se o que foi enviado na requisição condiz com o que é solicitado no endpoint
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Pega o campo id do personagem passado no corpo da requisição
            int idPersonagem = personagemAtualizado.Id;

            // Busca o personagem ja existente no banco de acordo com o id
            var personagemExistente = await _appDbContext.PERSONAGEM.FindAsync(idPersonagem);

            if (personagemExistente == null)
            {
                return NotFound("Personagem não encontrado!");
            }
            else
            {
                // Busca os valores dos campos do personagem ja existente e seta os valores do personagem atualizado
                _appDbContext.Entry(personagemExistente).CurrentValues.SetValues(personagemAtualizado);
                await _appDbContext.SaveChangesAsync();

                return Ok(personagemExistente);
            }
        }

        // Deleta personagem de acordo com id
        // Espera id na url
        [HttpDelete("deleteById/{id}")]
        public async Task<IActionResult> DeletePersonagem(int id)
        {
            // Itera sobre o resultado em busca do id passado como parâmetro
            var personagem = await _appDbContext.PERSONAGEM.FindAsync(id);

            if (personagem == null)
            {
                return NotFound("Personagem não encontrado!");
            }

            _appDbContext.PERSONAGEM.Remove(personagem);
            await _appDbContext.SaveChangesAsync();

            return Ok("Personagem deletado com sucesso!");
        }
    }
}