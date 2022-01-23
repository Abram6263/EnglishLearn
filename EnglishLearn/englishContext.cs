using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EnglishLearn
{
    public partial class englishContext : DbContext
    {
        public englishContext()
        {
        }

        public englishContext(DbContextOptions<englishContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Doptable> Doptables { get; set; }
        public virtual DbSet<Verb> Verbs { get; set; }
        public virtual DbSet<Word> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=english;Username=postgres;Password=6263");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doptable>(entity =>
            {
                entity.ToTable("doptable");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnglishWord)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("english_word");

                entity.Property(e => e.RussianWord)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("russian_word");
            });

            modelBuilder.Entity<Verb>(entity =>
            {
                entity.ToTable("verbs");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EnglishWord)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("english_word");

                entity.Property(e => e.RussianWord)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("russian_word");
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.ToTable("words");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tranclate)
                    .HasMaxLength(128)
                    .HasColumnName("tranclate");

                entity.Property(e => e.Word1)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("word");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
