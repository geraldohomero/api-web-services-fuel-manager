using api_web_services_fuel_manager.Models;
using BCrypt.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")] //rota de acesso para a api [controller] igual a usuarios
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context; // contexto de dados

        // recebe o "serviço" de banco de dados
        public UsuariosController(AppDbContext context)
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
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Usuario model)
        {
            _context.Usuarios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios
                .FirstOrDefaultAsync(c => c.Id == id);

            if (model == null) return NotFound();

            GerarLinks(model);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Usuario model)
        {
            if (id != model.Id) return BadRequest(); // se o id for diferente

            // consulta o banco de dados
            var modeloDb = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (modeloDb == null) return NotFound();

            _context.Usuarios.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Usuarios
                .FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        private void GerarLinks(Usuario model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "Delete"));
        }
    }

}
