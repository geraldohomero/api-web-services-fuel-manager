using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace api_web_services_fuel_manager.Models
{
    // está sendo usado como interface para as requisições, pois não irá retornar password. Será usado somente
    // para criar o usuário
    public class UsuarioDto
    {
        public int? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        // nao será ignorado o password
        public string Password { get; set; }
        [Required]
        public int Perfil { get; set; }
    }
}
