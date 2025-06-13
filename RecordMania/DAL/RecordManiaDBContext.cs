using Microsoft.EntityFrameworkCore;
using RecordMania.Models;
using Task = RecordMania.Models.Task;

namespace RecordMania.DAL;

public class RecordManiaDBContext : DbContext
{
    public RecordManiaDBContext(DbContextOptions<RecordManiaDBContext> options) : base(options) { }

    public DbSet<Student> Student { get; set; }
    
    public DbSet<Task> Task { get; set; }

    public DbSet<Language> Language { get; set; }
    
    public DbSet<Record> Record { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Record>()   
            .Property(r => r.ExecutionTime)   
            .HasColumnType("bigint");
    }
}