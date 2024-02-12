using api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")] //rota de acesso para a api [controller] igual a veiculos
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context; // contexto de dados

        // recebe o "serviço" de banco de dados
        public VeiculosController(AppDbContext context) 
        {
            //injeção de dependência
            _context = context; 
        }
        // no ASP.NET para o retorno das operações usa-se o "ActionResult". Usando ele
        // para retornar um novo tipo de dado. Ou seja, Não preciso pegar a resposta
        // do banco de dados e converter ele em JSON...Ele configura diretamente através das
        // views, ou nesse caso...JSON. Já sai configurado com esse modelo de resposta
        // também consegue configurar a resposta de erro
        [HttpGet]
        //     Resultado da requisição HTTP ou Rest
        //                      |
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            // int deixa um valor padrão de 0, diferente de strings que é "null", por isso as requisições serão aceitas caso seja int
            if(model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano de Fabricação e Modelo são obrigatórios e devem ser maior que ZERO" });
            }
            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest(); // se o id for diferente

            // consulta o banco de dados
            var modeloDb = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDb == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Veiculos
                .FindAsync(id);

            if (model == null) NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
           
        }
    }
}
