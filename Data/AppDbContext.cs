using Microsoft.EntityFrameworkCore;
using TesteCSharp.Models;

namespace TesteCSharp.Data
{
    // Classe para realizar conexão com banco de dados
    public class AppDbContext : DbContext
    {
        // Construtor da classe
        public AppDbContext(DbContextOptions options) : base(options){}

        // Cria a tabela PERSONAGEM no banco
        public DbSet<Personagem> PERSONAGEM{ get; set; }
    }
}