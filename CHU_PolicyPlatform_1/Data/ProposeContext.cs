using System;
using CHU_PolicyPlatform_1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace CHU_PolicyPlatform_1.Data
{
    public partial class ProposeContext : DbContext
    {
        public ProposeContext()
        {
        }

        public ProposeContext(DbContextOptions<ProposeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Gerent> Gerents { get; set; }
        public virtual DbSet<Proposal> Proposals { get; set; }
        public virtual DbSet<ToRepond> ToReponds { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Vote> Votes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Database=Propose;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.CategoryId)
                    .HasMaxLength(10)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Gerent>(entity =>
            {
                entity.ToTable("Gerent");

                entity.Property(e => e.GerentId)
                    .HasMaxLength(10)
                    .HasColumnName("GerentID");

                entity.Property(e => e.Gpassword)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("GPassword");
            });

            modelBuilder.Entity<Proposal>(entity =>
            {
                entity.ToTable("Proposal");

                entity.Property(e => e.ProposalId)
                    .HasMaxLength(10)
                    .HasColumnName("ProposalID");

                entity.Property(e => e.CategoryId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("CategoryID");

                entity.Property(e => e.GainsInfluential)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Gains&Influential");

                entity.Property(e => e.Pcontent)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("PContent");

                entity.Property(e => e.Pdate)
                    .HasColumnType("datetime")
                    .HasColumnName("PDate");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_Category");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proposal_User");
            });

            modelBuilder.Entity<ToRepond>(entity =>
            {
                entity.HasKey(e => new { e.ProposalId, e.GerentId });

                entity.ToTable("ToRepond");

                entity.Property(e => e.ProposalId)
                    .HasMaxLength(10)
                    .HasColumnName("ProposalID");

                entity.Property(e => e.GerentId)
                    .HasMaxLength(10)
                    .HasColumnName("GerentID");

                entity.Property(e => e.Rcontent)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("RContent");

                entity.HasOne(d => d.Gerent)
                    .WithMany(p => p.ToReponds)
                    .HasForeignKey(d => d.GerentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToRepond_Gerent");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.ToReponds)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToRepond_Proposal");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("UserID");

                entity.Property(e => e.Upassword)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("UPassword");
            });

            modelBuilder.Entity<Vote>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProposalId });

                entity.ToTable("Vote");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("UserID");

                entity.Property(e => e.ProposalId)
                    .HasMaxLength(10)
                    .HasColumnName("ProposalID");

                entity.Property(e => e.Vcontent)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("VContent");

                entity.Property(e => e.Vdate)
                    .HasColumnType("datetime")
                    .HasColumnName("VDate");

                entity.HasOne(d => d.Proposal)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.ProposalId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_Proposal");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Votes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vote_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
