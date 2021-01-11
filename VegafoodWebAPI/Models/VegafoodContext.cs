using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace VegafoodWebAPI.Models
{
    public partial class VegafoodContext : DbContext
    {
        public VegafoodContext()
        {
        }

        public VegafoodContext(DbContextOptions<VegafoodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlphabeticalListOfProducts> AlphabeticalListOfProducts { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997 { get; set; }
        public virtual DbSet<CurrentProductList> CurrentProductList { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<OrderDetails> OrderDetails { get; set; }
        public virtual DbSet<OrderDetailsExtended> OrderDetailsExtended { get; set; }
        public virtual DbSet<OrderSubtotals> OrderSubtotals { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Payments> Payments { get; set; }
        public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997 { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrice { get; set; }
        public virtual DbSet<SalesByCategory> SalesByCategory { get; set; }
        public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmount { get; set; }
        public virtual DbSet<Shippers> Shippers { get; set; }
        public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarter { get; set; }
        public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYear { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=Vegafood");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlphabeticalListOfProducts>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Alphabetical list of products");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Images).HasColumnType("image");

                entity.Property(e => e.ImagesPath).HasMaxLength(50);

                entity.Property(e => e.ProductDesc).HasMaxLength(500);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Picture).HasColumnType("image");
            });

            modelBuilder.Entity<CategorySalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Category Sales for 1997");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.CategorySales).HasColumnType("money");
            });

            modelBuilder.Entity<CurrentProductList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Current Product List");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.ContactName).HasMaxLength(30);

                entity.Property(e => e.ContactTitle).HasMaxLength(30);

                entity.Property(e => e.District).HasMaxLength(250);

                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Address).HasMaxLength(60);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.City).HasMaxLength(15);

                entity.Property(e => e.Country).HasMaxLength(15);

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.HireDate).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Notes).HasColumnType("ntext");

                entity.Property(e => e.Phone).HasMaxLength(24);

                entity.Property(e => e.Photo).HasColumnType("image");

                entity.Property(e => e.Title).HasMaxLength(30);

                entity.Property(e => e.TitleOfCourtesy).HasMaxLength(25);

                entity.HasOne(d => d.ReportsToNavigation)
                    .WithMany(p => p.InverseReportsToNavigation)
                    .HasForeignKey(d => d.ReportsTo)
                    .HasConstraintName("FK_Employees_Employees");
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId })
                    .HasName("PK_Order_Details");

                entity.ToTable("Order Details");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Details_Products");
            });

            modelBuilder.Entity<OrderDetailsExtended>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Details Extended");

                entity.Property(e => e.ExtendedPrice).HasColumnType("money");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<OrderSubtotals>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Order Subtotals");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.ShipAddress).HasMaxLength(60);

                entity.Property(e => e.ShipCity).HasMaxLength(15);

                entity.Property(e => e.ShipDistrict).HasMaxLength(15);

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.PaymentViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentVia)
                    .HasConstraintName("FK_Orders_Payments");

                entity.HasOne(d => d.ShipViaNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShipVia)
                    .HasConstraintName("FK_Orders_Shippers");
            });

            modelBuilder.Entity<Payments>(entity =>
            {
                entity.HasKey(e => e.PaymentId);

                entity.Property(e => e.PaymentId)
                    .HasColumnName("PaymentID")
                    .ValueGeneratedNever();

                entity.Property(e => e.PayCompany).HasMaxLength(50);

                entity.Property(e => e.PaymentSolution).HasMaxLength(50);
            });

            modelBuilder.Entity<ProductSalesFor1997>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Product Sales for 1997");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ProductSales).HasColumnType("money");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Images).HasColumnType("image");

                entity.Property(e => e.ImagesPath).HasMaxLength(50);

                entity.Property(e => e.ProductDescription).HasMaxLength(500);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice)
                    .HasColumnType("money")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UnitsOnOrder).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Products_Categories");
            });

            modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Products Above Average Price");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.UnitPrice).HasColumnType("money");
            });

            modelBuilder.Entity<SalesByCategory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales by Category");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.ProductSales).HasColumnType("money");
            });

            modelBuilder.Entity<SalesTotalsByAmount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Sales Totals by Amount");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.SaleAmount).HasColumnType("money");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Shippers>(entity =>
            {
                entity.HasKey(e => e.ShipperId);

                entity.Property(e => e.ShipperId).HasColumnName("ShipperID");

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Phone).HasMaxLength(24);
            });

            modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Quarter");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("Summary of Sales by Year");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ShippedDate).HasColumnType("datetime");

                entity.Property(e => e.Subtotal).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
