using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace api_web_services_fuel_manager.Models
{
    [Table("Usuários")]
    public class Usuario :LinksHATEOS
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [JsonIgnore] // para ignorar o password e não ser retornardo na API 
        public string Password { get; set; }
        [Required]
        public int Perfil { get; set; }

        public ICollection<VeiculoUsuarios> Veiculos { get; set; }

    }
    public enum Perfil
    {
        [Display(Name = "Administrador")]
        Administrador,
        [Display(Name = "Usuário")]
        Usuario
    }
}
