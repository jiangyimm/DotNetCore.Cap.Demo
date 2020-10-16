using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Cap.Demo.Publisher.PgModels
{
    public partial class PgDbContext : DbContext
    {
        public PgDbContext()
        {
        }

        public PgDbContext(DbContextOptions<PgDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApiConfig> ApiConfig { get; set; }
        public virtual DbSet<ApiConfigParam> ApiConfigParam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;UserId=jiangyi;Password=jiangyi;Database=cap_test;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiConfig>(entity =>
            {
                entity.ToTable("api_config", "test_platform");

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('test_platform.\"api_config_Id_seq\"'::regclass)");

                entity.Property(e => e.ApiDesc).HasMaxLength(64);

                entity.Property(e => e.ApiName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.OperCode).HasMaxLength(16);

                entity.Property(e => e.ReturnType)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<ApiConfigParam>(entity =>
            {
                entity.ToTable("api_config_param", "test_platform");

                entity.HasIndex(e => e.ApiConfigId);

                entity.Property(e => e.Id).HasDefaultValueSql("nextval('test_platform.\"api_config_param_Id_seq\"'::regclass)");

                entity.Property(e => e.OperCode).HasMaxLength(16);

                entity.Property(e => e.ParamDefaultValue).HasMaxLength(255);

                entity.Property(e => e.ParamDesc).HasMaxLength(255);

                entity.Property(e => e.ParamName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ParamType)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.HasOne(d => d.ApiConfig)
                    .WithMany(p => p.ApiConfigParam)
                    .HasForeignKey(d => d.ApiConfigId);
            });

            modelBuilder.HasSequence("api_config_Id_seq");

            modelBuilder.HasSequence("api_config_param_Id_seq");
        }
    }
}
