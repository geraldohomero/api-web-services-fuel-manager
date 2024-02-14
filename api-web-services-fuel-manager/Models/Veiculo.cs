using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_services_fuel_manager.Models;

//da nome para as tabelas
[Table("Veículos")]
public class Veiculo : LinksHATEOS
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Marca { get; set; }
    [Required]
    public string Modelo { get; set; }
    [Required]
    public string Placa { get; set; }
    [Required]
    public int AnoFabricacao { get; set; }
    [Required]
    public int AnoModelo { get; set; }

    // não é necessário ter nenhuma propriedade de consumo para 
    // fazer a ligação da foreign key, mas pode ter a navegação virtual
    // esse veículo agora tem uma "coleção de consumos"
    public ICollection<Consumo> Consumos { get; set; }
    // um veiculo possui vários consumos, e um consumo está associado 
    // somente a um veículo

    public ICollection<VeiculoUsuarios> Usuarios { get; set; }
}