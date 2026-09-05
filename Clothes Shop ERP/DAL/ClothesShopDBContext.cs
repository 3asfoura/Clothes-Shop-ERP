using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clothes_Shop_ERP.DAL
{
    public partial class ClothesShopDBContext : DbContext
    {
        public ClothesShopDBContext()
        {
        }

        public ClothesShopDBContext(DbContextOptions<ClothesShopDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditLogs> AuditLogs { get; set; }
        public virtual DbSet<Branches> Branches { get; set; }
        public virtual DbSet<BranchStock> BranchStock { get; set; }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<PaymentMethods> PaymentMethods { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductVariants> ProductVariants { get; set; }
        public virtual DbSet<PurchaseInvoiceDetails> PurchaseInvoiceDetails { get; set; }
        public virtual DbSet<PurchaseInvoices> PurchaseInvoices { get; set; }
        public virtual DbSet<PurchaseReturnDetails> PurchaseReturnDetails { get; set; }
        public virtual DbSet<PurchaseReturns> PurchaseReturns { get; set; }
        public virtual DbSet<RolePermissions> RolePermissions { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<SalesInvoiceDetails> SalesInvoiceDetails { get; set; }
        public virtual DbSet<SalesInvoices> SalesInvoices { get; set; }
        public virtual DbSet<SalesReturnDetails> SalesReturnDetails { get; set; }
        public virtual DbSet<SalesReturns> SalesReturns { get; set; }
        public virtual DbSet<Sizes> Sizes { get; set; }
        public virtual DbSet<StockMovements> StockMovements { get; set; }
        public virtual DbSet<StockTransferDetails> StockTransferDetails { get; set; }
        public virtual DbSet<StockTransfers> StockTransfers { get; set; }
        public virtual DbSet<Suppliers> Suppliers { get; set; }
        public virtual DbSet<TreasuryTransactions> TreasuryTransactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Sett.cn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ChangedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.RecordId).HasMaxLength(50);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Branches>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });

            modelBuilder.Entity<BranchStock>(entity =>
            {
                entity.HasIndex(e => new { e.ProductVariantId, e.BranchId })
                    .HasName("UQ_Stock_VariantBranch")
                    .IsUnique();

                entity.Property(e => e.MinQuantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchStock)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Branch");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.BranchStock)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stock_Variant");
            });

            modelBuilder.Entity<Brands>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Brands__737584F668FD1EF2")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.ParentCategory)
                    .WithMany(p => p.InverseParentCategory)
                    .HasForeignKey(d => d.ParentCategoryId)
                    .HasConstraintName("FK_Category_Parent");
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Colors__737584F6F66BDF6F")
                    .IsUnique();

                entity.Property(e => e.HexCode).HasMaxLength(7);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });

            modelBuilder.Entity<PaymentMethods>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__PaymentM__737584F654761E69")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasIndex(e => e.CategoryId)
                    .HasName("IX_Product_Category");

                entity.HasIndex(e => e.Code)
                    .HasName("UQ__Products__A25C5AA72A739D86")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Product_Brand");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Category");
            });

            modelBuilder.Entity<ProductVariants>(entity =>
            {
                entity.HasIndex(e => e.Barcode)
                    .HasName("IX_Variant_Barcode");

                entity.HasIndex(e => new { e.ProductId, e.ColorId, e.SizeId })
                    .HasName("UQ_Variant_Combo")
                    .IsUnique();

                entity.Property(e => e.Barcode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Color)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(d => d.ColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variant_Color");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variant_Product");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.ProductVariants)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Variant_Size");
            });

            modelBuilder.Entity<PurchaseInvoiceDetails>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.PurchaseInvoiceDetails)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PDetail_Variant");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.PurchaseInvoiceDetails)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PDetail_Invoice");
            });

            modelBuilder.Entity<PurchaseInvoices>(entity =>
            {
                entity.Property(e => e.InvoiceDate).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Completed')");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PInvoice_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PInvoice_User");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.PurchaseInvoices)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PInvoice_Supplier");
            });

            modelBuilder.Entity<PurchaseReturnDetails>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.PurchaseReturnDetails)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRDetail_Variant");

                entity.HasOne(d => d.PurchaseReturn)
                    .WithMany(p => p.PurchaseReturnDetails)
                    .HasForeignKey(d => d.PurchaseReturnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PRDetail_Return");
            });

            modelBuilder.Entity<PurchaseReturns>(entity =>
            {
                entity.Property(e => e.ReturnDate).HasDefaultValueSql("(sysutcdatetime())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.PurchaseReturns)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PReturn_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.PurchaseReturns)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PReturn_User");

                entity.HasOne(d => d.PurchaseInvoice)
                    .WithMany(p => p.PurchaseReturns)
                    .HasForeignKey(d => d.PurchaseInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PReturn_Invoice");
            });

            modelBuilder.Entity<RolePermissions>(entity =>
            {
                entity.HasIndex(e => new { e.RoleId, e.ScreenName })
                    .HasName("UQ_RolePermissions_RoleScreen")
                    .IsUnique();

                entity.Property(e => e.PermissionLevel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('None')");

                entity.Property(e => e.ScreenName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RolePermissions_Role");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Roles__737584F601354225")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SalesInvoiceDetails>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.SalesInvoiceDetails)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SDetail_Variant");

                entity.HasOne(d => d.SalesInvoice)
                    .WithMany(p => p.SalesInvoiceDetails)
                    .HasForeignKey(d => d.SalesInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SDetail_Invoice");
            });

            modelBuilder.Entity<SalesInvoices>(entity =>
            {
                entity.HasIndex(e => e.InvoiceNumber)
                    .HasName("UQ__SalesInv__D776E981E39F37D0")
                    .IsUnique();

                entity.HasIndex(e => new { e.BranchId, e.InvoiceDate })
                    .HasName("IX_SalesInvoice_Branch");

                entity.Property(e => e.InvoiceDate).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.InvoiceNumber)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Completed')");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SInvoice_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SInvoice_User");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_SInvoice_Customer");

                entity.HasOne(d => d.PaymentMethod)
                    .WithMany(p => p.SalesInvoices)
                    .HasForeignKey(d => d.PaymentMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SInvoice_Payment");
            });

            modelBuilder.Entity<SalesReturnDetails>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.SalesReturnDetails)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RDetail_Variant");

                entity.HasOne(d => d.SalesReturn)
                    .WithMany(p => p.SalesReturnDetails)
                    .HasForeignKey(d => d.SalesReturnId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RDetail_Return");
            });

            modelBuilder.Entity<SalesReturns>(entity =>
            {
                entity.Property(e => e.ReturnDate).HasDefaultValueSql("(sysutcdatetime())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Return_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Return_User");

                entity.HasOne(d => d.SalesInvoice)
                    .WithMany(p => p.SalesReturns)
                    .HasForeignKey(d => d.SalesInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Return_Invoice");
            });

            modelBuilder.Entity<Sizes>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Sizes__737584F62D039466")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StockMovements>(entity =>
            {
                entity.HasIndex(e => new { e.ProductVariantId, e.BranchId })
                    .HasName("IX_Movement_Variant");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.MovementType)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.Property(e => e.RefType).HasMaxLength(30);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.StockMovements)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.StockMovements)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_User");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.StockMovements)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movement_Variant");
            });

            modelBuilder.Entity<StockTransferDetails>(entity =>
            {
                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 3)");

                entity.HasOne(d => d.ProductVariant)
                    .WithMany(p => p.StockTransferDetails)
                    .HasForeignKey(d => d.ProductVariantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetail_Variant");

                entity.HasOne(d => d.StockTransfer)
                    .WithMany(p => p.StockTransferDetails)
                    .HasForeignKey(d => d.StockTransferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransferDetail_Transfer");
            });

            modelBuilder.Entity<StockTransfers>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.StockTransfers)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfer_User");

                entity.HasOne(d => d.FromBranch)
                    .WithMany(p => p.StockTransfersFromBranch)
                    .HasForeignKey(d => d.FromBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfer_FromBranch");

                entity.HasOne(d => d.ToBranch)
                    .WithMany(p => p.StockTransfersToBranch)
                    .HasForeignKey(d => d.ToBranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfer_ToBranch");
            });

            modelBuilder.Entity<Suppliers>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Phone).HasMaxLength(30);
            });

            modelBuilder.Entity<TreasuryTransactions>(entity =>
            {
                entity.HasIndex(e => new { e.BranchId, e.CreatedAt })
                    .HasName("IX_Treasury_Branch");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.RefType).HasMaxLength(30);

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TreasuryTransactions)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treasury_Branch");

                entity.HasOne(d => d.CreatedByUser)
                    .WithMany(p => p.TreasuryTransactions)
                    .HasForeignKey(d => d.CreatedByUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Treasury_User");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Users__536C85E48F49748D")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysutcdatetime())");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK_Users_Branch");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Role");
            });
        }
    }
}
