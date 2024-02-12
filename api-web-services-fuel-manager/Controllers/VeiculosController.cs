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
    }
}
