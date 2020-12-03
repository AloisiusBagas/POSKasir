using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PosKasir.Entities.Entities.postGre
{
    public partial class PostGreDbContext : DbContext
    {
        public PostGreDbContext(DbContextOptions<PostGreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbSemen> TbSemen { get; set; }
        public virtual DbSet<TbTransaction> TbTransaction { get; set; }
        public virtual DbSet<TbTransactionDetail> TbTransactionDetail { get; set; }
        public virtual DbSet<TbUser> TbUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbSemen>(entity =>
            {
                entity.Property(e => e.SemenId).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("'SYSTEM'::character varying");
            });

            modelBuilder.Entity<TbTransaction>(entity =>
            {
                entity.Property(e => e.TransactionId).IsFixedLength();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("'SYSTEM'::character varying");

                entity.Property(e => e.TransacionDate).HasDefaultValueSql("now()");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbTransaction)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbTransaction_TbUser");
            });

            modelBuilder.Entity<TbTransactionDetail>(entity =>
            {
                entity.Property(e => e.TransactionDetailId).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("'SYSTEM'::character varying");

                entity.Property(e => e.TransactionId).IsFixedLength();

                entity.HasOne(d => d.Semen)
                    .WithMany(p => p.TbTransactionDetail)
                    .HasForeignKey(d => d.SemenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbTransactionDetail_TbSemen");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TbTransactionDetail)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbTransactionDetail_TbTransaction");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TbTransactionDetail)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TbTransactionDetail_TbUser");
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.Property(e => e.UserId).UseIdentityAlwaysColumn();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("now()");

                entity.Property(e => e.CreatedBy).HasDefaultValueSql("'SYSTEM'::character varying");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
