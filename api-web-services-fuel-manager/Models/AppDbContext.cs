using Microsoft.EntityFrameworkCore;

namespace api_web_services_fuel_manager.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {  
        }

        // relacionamento n - n VeiculoUsuarios.cs
        protected override void OnModelCreating(ModelBuilder builder) // Quando há a criação de um modelo, pode-se configurar as suas relações
        {
            builder.Entity<VeiculoUsuarios>() // a entidade que será configurada = VeiculoUsuarios
                .HasKey(c => new { c.VeiculoId, c.UsuarioId }); // coloca uma chave composta na entidade

            // criando as ForeignKeys
            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Veiculo).WithMany(c => c.Usuarios) // para cada veículo pode-se ter vários usuários
                .HasForeignKey(c => c.VeiculoId);

            builder.Entity<VeiculoUsuarios>()
                .HasOne(c => c.Usuario).WithMany(c => c.Veiculos) // para cada usuário pode-se ter vários veículos
                .HasForeignKey(c => c.UsuarioId);

        }

        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Consumo> Consumos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VeiculoUsuarios> VeiculosUsuarios { get; set; }
    }
}
