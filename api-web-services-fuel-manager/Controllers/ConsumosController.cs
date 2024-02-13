using api_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")] //rota de acesso para a api [controller] igual a consumos
    [ApiController]
    public class ConsumosController : ControllerBase
    {
        private readonly AppDbContext _context; // contexto de dados

        // recebe o "serviço" de banco de dados
        public ConsumosController(AppDbContext context)
        {
            //injeção de dependência
            _context = context;
        }
       
        [HttpGet]
        //     Resultado da requisição HTTP ou Rest
        //                     |
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Consumos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Consumo model)
        {
            _context.Consumos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Consumos
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Consumo model)
        {
            if (id != model.Id) return BadRequest(); 

            var modeloDb = await _context.Consumos.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDb == null) return NotFound();

            _context.Consumos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Consumos
                .FindAsync(id);

            if (model == null) return NotFound();

            _context.Consumos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private void GerarLinks(Consumo model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
    }
}

