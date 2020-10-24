using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using SCAWeb.Service.Monitoramento.Entities;

namespace SCAWeb.Service.Monitoramento.Data
{
    public class MonitoramentoContext : DbContext
    {
        public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options)
            : base(options)
        {
        }

        public DbSet<AreaEntity> Areas { get; set; }
        public DbSet<SensorEntity> Sensores { get; set; }
        public DbSet<AlertaSensorEntity> Alertas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();

            modelBuilder.Entity<AreaEntity>().ToTable("Areas");
            modelBuilder.Entity<AreaEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AreaEntity>().Property(x => x.user).IsRequired();
            modelBuilder.Entity<AreaEntity>().Property(x => x.nome_barragem).IsRequired();
            modelBuilder.Entity<AreaEntity>().Property(x => x.data_atualizacao).HasColumnType("datetime").IsRequired();

            modelBuilder.Entity<SensorEntity>().ToTable("Sensores");
            modelBuilder.Entity<SensorEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<SensorEntity>().Property(x => x.user).IsRequired();
            modelBuilder.Entity<SensorEntity>().Property(x => x.nome_sensor).IsRequired();
            modelBuilder.Entity<SensorEntity>().Property(x => x.status_sensor).IsRequired();
            modelBuilder.Entity<SensorEntity>().Property(x => x.data_atualizacao).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<SensorEntity>().Property(x => x.id_area).IsRequired();

            modelBuilder.Entity<AlertaSensorEntity>().ToTable("Fornecedor");
            modelBuilder.Entity<AlertaSensorEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<AlertaSensorEntity>().Property(x => x.user).IsRequired();
            modelBuilder.Entity<AlertaSensorEntity>().Property(x => x.tipo_aletra).IsRequired();
            modelBuilder.Entity<AlertaSensorEntity>().Property(x => x.data_atualizacao).HasColumnType("datetime").IsRequired();
            modelBuilder.Entity<AlertaSensorEntity>().Property(x => x.id_sensor).IsRequired();
        }

    }
}
