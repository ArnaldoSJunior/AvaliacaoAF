using Microsoft.EntityFrameworkCore;

namespace ARNALDO.Models;

public class AppDataContext : DbContext
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<IMC> Imc { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Arnaldo.db");
    }
}