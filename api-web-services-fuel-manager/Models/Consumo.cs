using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_services_fuel_manager.Models;

//da nome para tabelas
[Table("Consumos")]
public class Consumo : LinksHATEOS
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Descricao { get; set; }
    [Required]
    public DateTime Data { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }
    [Required]
    public TipoCombustivel Tipo { get; set; }

    // um veículo tem n consumos, um relacionamento de 1 para n
    // notação do ef 
    [Required]
    public int VeiculoId { get; set; } //ForeignKey

    // navegação virtualk
    // o ef quando for carregar as informações do consumo
    // ele pode carregar todas as informações do veículo
    // associado a esse consumo. Navegar (de uma entidade para outra)
    // de forma mais simples.
    public Veiculo Veiculo { get; set; } // navegação virtual
}

public enum TipoCombustivel
{
    Diesel,
    Etanol,
    Gasolina
}