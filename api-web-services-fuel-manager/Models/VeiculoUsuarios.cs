using System.ComponentModel.DataAnnotations.Schema;

namespace api_web_services_fuel_manager.Models
{
    // Tabela intermediária, uma vez que não podemos colocar o Id de
    // veículo na tabela Usuario.cs uma vez que são vários veículos, nem mesmo 
    // um Id de usuário na tabela Veiculo.cs, pois podem haver vários usuários.
    // Por isso usa-se a tabela intermediária

    // É um relacionamento n - n 
    [Table("VeiculoUsuarios")] // todos os usuários relacionados a um determinado veículo
    public class VeiculoUsuarios
    {
        public int VeiculoId { get; set; }
        public Veiculo Veiculo { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
