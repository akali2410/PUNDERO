using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PUNDERO.Models;

public partial class PunderoContext : DbContext
{
    public PunderoContext()
    {
    }

    public PunderoContext(DbContextOptions<PunderoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AssignmentType> AssignmentTypes { get; set; }

    public virtual DbSet<AuthenticationToken> AuthenticationTokens { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Coordinator> Coordinators { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<InvoiceProduct> InvoiceProducts { get; set; }

    public virtual DbSet<InvoiceStatus> InvoiceStatuses { get; set; }

    public virtual DbSet<Mobile> Mobiles { get; set; }

    public virtual DbSet<MobileDriver> MobileDrivers { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Tachograph> Tachographs { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<VehicleDriver> VehicleDrivers { get; set; }

    public virtual DbSet<Warehouse> Warehouses { get; set; }

    public virtual DbSet<WorkingHour> WorkingHours { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-JOEL5NL\\SQLEXPRESS;Database=PUNDERO;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.IdAccount);

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FIRST_NAME");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("IMAGE");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("LAST_NAME");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.Type).HasColumnName("TYPE");
        });

        modelBuilder.Entity<AssignmentType>(entity =>
        {
            entity.HasKey(e => e.IdAssignmentType);

            entity.ToTable("ASSIGNMENT_TYPE");

            entity.Property(e => e.IdAssignmentType).HasColumnName("ID_ASSIGNMENT_TYPE");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<AuthenticationToken>(entity =>
        {
            entity.HasKey(e => e.IdAuthentication);

            entity.ToTable("AUTHENTICATION_TOKEN");

            entity.Property(e => e.IdAuthentication).HasColumnName("ID_AUTHENTICATION");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");
            entity.Property(e => e.SignDate)
                .HasColumnType("date")
                .HasColumnName("SIGN_DATE");
            entity.Property(e => e.TokenValue)
                .HasMaxLength(50)
                .HasColumnName("TOKEN_VALUE");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.AuthenticationTokens)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_AUTHENTICATION_TOKEN_ACCOUNT");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient);

            entity.ToTable("CLIENT");

            entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");
            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Clients)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_CLIENT_ACCOUNT");
        });

        modelBuilder.Entity<Coordinator>(entity =>
        {
            entity.HasKey(e => e.IdCoordinator);

            entity.ToTable("COORDINATOR");

            entity.Property(e => e.IdCoordinator).HasColumnName("ID_COORDINATOR");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");
            entity.Property(e => e.Qualification)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("QUALIFICATION");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Coordinators)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_COORDINATOR_ACCOUNT");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(e => e.IdDriver);

            entity.ToTable("DRIVER");

            entity.Property(e => e.IdDriver).HasColumnName("ID_DRIVER");
            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");
            entity.Property(e => e.IdTachograph).HasColumnName("ID_TACHOGRAPH");
            entity.Property(e => e.LicenseCategory)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("LICENSE_CATEGORY");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("LICENSE_NUMBER");
            entity.Property(e => e.PrivateMobile).HasColumnName("PRIVATE_MOBILE");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK_DRIVER_ACCOUNT");

            entity.HasOne(d => d.IdTachographNavigation).WithMany(p => p.Drivers)
                .HasForeignKey(d => d.IdTachograph)
                .HasConstraintName("FK_DRIVER_TACHOGRAPH");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.IdInvoice);

            entity.ToTable("INVOICE");

            entity.Property(e => e.IdInvoice).HasColumnName("ID_INVOICE");
            entity.Property(e => e.IdDriver).HasColumnName("ID_DRIVER");
            entity.Property(e => e.IdStatus).HasColumnName("ID_STATUS");
            entity.Property(e => e.IdStore).HasColumnName("ID_STORE");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_WAREHOUSE");
            entity.Property(e => e.IssueDate)
                .HasColumnType("datetime")
                .HasColumnName("ISSUE_DATE");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NOTE");

            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdDriver)
                .HasConstraintName("FK_INVOICE_DRIVER");

            entity.HasOne(d => d.IdStatusNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdStatus)
                .HasConstraintName("FK_INVOICE_INVOICE_STATUS");

            entity.HasOne(d => d.IdStoreNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdStore)
                .HasConstraintName("FK_INVOICE_OBJECT");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK_INVOICE_WAREHOUSE");
        });

        modelBuilder.Entity<InvoiceProduct>(entity =>
        {
            entity.HasKey(e => e.IdInvoiceProduct);

            entity.ToTable("INVOICE_PRODUCT");

            entity.Property(e => e.IdInvoiceProduct).HasColumnName("ID_INVOICE_PRODUCT");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_INVOICE");
            entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");
            entity.Property(e => e.OrderQuantity).HasColumnName("ORDER_QUANTITY");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.IdInvoice)
                .HasConstraintName("FK_INVOICE_PRODUCT_INVOICE");

            entity.HasOne(d => d.IdProductNavigation).WithMany(p => p.InvoiceProducts)
                .HasForeignKey(d => d.IdProduct)
                .HasConstraintName("FK_INVOICE_PRODUCT_PRODUCT");
        });

        modelBuilder.Entity<InvoiceStatus>(entity =>
        {
            entity.HasKey(e => e.IdStatus);

            entity.ToTable("INVOICE_STATUS");

            entity.Property(e => e.IdStatus).HasColumnName("ID_STATUS");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
        });

        modelBuilder.Entity<Mobile>(entity =>
        {
            entity.HasKey(e => e.IdMobile);

            entity.ToTable("MOBILE");

            entity.Property(e => e.IdMobile).HasColumnName("ID_MOBILE");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BRAND");
            entity.Property(e => e.Imei)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IMEI");
            entity.Property(e => e.LkLatitude).HasColumnName("LK_LATITUDE");
            entity.Property(e => e.LkLongitude).HasColumnName("LK_LONGITUDE");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.PhoneNumber).HasColumnName("PHONE_NUMBER");
        });

        modelBuilder.Entity<MobileDriver>(entity =>
        {
            entity.HasKey(e => e.IdMobileDriver);

            entity.ToTable("MOBILE_DRIVER");

            entity.Property(e => e.IdMobileDriver).HasColumnName("ID_MOBILE_DRIVER");
            entity.Property(e => e.AssignmentEndDate)
                .HasColumnType("date")
                .HasColumnName("ASSIGNMENT_END_DATE");
            entity.Property(e => e.AssignmentStartDate)
                .HasColumnType("date")
                .HasColumnName("ASSIGNMENT_START_DATE");
            entity.Property(e => e.IdAssignmentType).HasColumnName("ID_ASSIGNMENT_TYPE");
            entity.Property(e => e.IdDriver).HasColumnName("ID_DRIVER");
            entity.Property(e => e.IdMobile).HasColumnName("ID_MOBILE");

            entity.HasOne(d => d.IdAssignmentTypeNavigation).WithMany(p => p.MobileDrivers)
                .HasForeignKey(d => d.IdAssignmentType)
                .HasConstraintName("FK_MOBILE_DRIVER_ASSIGNMENT_TYPE");

            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.MobileDrivers)
                .HasForeignKey(d => d.IdDriver)
                .HasConstraintName("FK_MOBILE_DRIVER_DRIVER");

            entity.HasOne(d => d.IdMobileNavigation).WithMany(p => p.MobileDrivers)
                .HasForeignKey(d => d.IdMobile)
                .HasConstraintName("FK_MOBILE_DRIVER_MOBILE");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.IdNotification).HasName("PK__NOTIFICA__B78BF54650FDF929");

            entity.ToTable("NOTIFICATION");

            entity.Property(e => e.IdNotification).HasColumnName("ID_NOTIFICATION");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_At");
            entity.Property(e => e.IdAccount).HasColumnName("ID_ACCOUNT");
            entity.Property(e => e.IdInvoice).HasColumnName("ID_INVOICE");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Seen)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SEEN");

            entity.HasOne(d => d.IdAccountNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdAccount)
                .HasConstraintName("FK__NOTIFICAT__ID_AC__0B5CAFEA");

            entity.HasOne(d => d.IdInvoiceNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.IdInvoice)
                .HasConstraintName("FK_NOTIFICATION_INVOICE");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct);

            entity.ToTable("PRODUCT");

            entity.Property(e => e.IdProduct).HasColumnName("ID_PRODUCT");
            entity.Property(e => e.Barcode).HasColumnName("BARCODE");
            entity.Property(e => e.IdWarehouse).HasColumnName("ID_WAREHOUSE");
            entity.Property(e => e.NameProduct)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NAME_PRODUCT");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.IdWarehouseNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdWarehouse)
                .HasConstraintName("FK_PRODUCT_WAREHOUSE");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.IdStore).HasName("PK_OBJECT");

            entity.ToTable("STORE");

            entity.Property(e => e.IdStore).HasColumnName("ID_STORE");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.IdClient).HasColumnName("ID_CLIENT");
            entity.Property(e => e.Latitude).HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Qr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("QR");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Stores)
                .HasForeignKey(d => d.IdClient)
                .HasConstraintName("FK_OBJECT_CLIENT");
        });

        modelBuilder.Entity<Tachograph>(entity =>
        {
            entity.HasKey(e => e.IdTachograph);

            entity.ToTable("TACHOGRAPH");

            entity.Property(e => e.IdTachograph).HasColumnName("ID_TACHOGRAPH");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("EXPIRY_DATE");
            entity.Property(e => e.IdWorkingHours).HasColumnName("ID_WORKING_HOURS");
            entity.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("ISSUE_DATE");
            entity.Property(e => e.Label)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LABEL");

            entity.HasOne(d => d.IdWorkingHoursNavigation).WithMany(p => p.Tachographs)
                .HasForeignKey(d => d.IdWorkingHours)
                .HasConstraintName("FK_TACHOGRAPH_WORKING_HOURS");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.IdVehicle);

            entity.ToTable("VEHICLE");

            entity.Property(e => e.IdVehicle).HasColumnName("ID_VEHICLE");
            entity.Property(e => e.Brand)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("BRAND");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("COLOR");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("date")
                .HasColumnName("EXPIRY_DATE");
            entity.Property(e => e.IssueDate)
                .HasColumnType("date")
                .HasColumnName("ISSUE_DATE");
            entity.Property(e => e.Model)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.Registration)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("REGISTRATION");
        });

        modelBuilder.Entity<VehicleDriver>(entity =>
        {
            entity.HasKey(e => e.IdVehicleDriver).HasName("PK_VEHICLE_DRIVER_1");

            entity.ToTable("VEHICLE_DRIVER");

            entity.Property(e => e.IdVehicleDriver).HasColumnName("ID_VEHICLE_DRIVER");
            entity.Property(e => e.AssignmentEndDate)
                .HasColumnType("date")
                .HasColumnName("ASSIGNMENT_END_DATE");
            entity.Property(e => e.AssignmentStartDate)
                .HasColumnType("date")
                .HasColumnName("ASSIGNMENT_START_DATE");
            entity.Property(e => e.IdAssignmentType).HasColumnName("ID_ASSIGNMENT_TYPE");
            entity.Property(e => e.IdDriver).HasColumnName("ID_DRIVER");
            entity.Property(e => e.IdVehicle).HasColumnName("ID_VEHICLE");

            entity.HasOne(d => d.IdAssignmentTypeNavigation).WithMany(p => p.VehicleDrivers)
                .HasForeignKey(d => d.IdAssignmentType)
                .HasConstraintName("FK_VEHICLE_DRIVER_ASSIGNMENT_TYPE");

            entity.HasOne(d => d.IdDriverNavigation).WithMany(p => p.VehicleDrivers)
                .HasForeignKey(d => d.IdDriver)
                .HasConstraintName("FK_VEHICLE_DRIVER_DRIVER");

            entity.HasOne(d => d.IdVehicleNavigation).WithMany(p => p.VehicleDrivers)
                .HasForeignKey(d => d.IdVehicle)
                .HasConstraintName("FK_VEHICLE_DRIVER_VEHICLE1");
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(e => e.IdWarehouse);

            entity.ToTable("WAREHOUSE");

            entity.Property(e => e.IdWarehouse).HasColumnName("ID_WAREHOUSE");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Latitude).HasColumnName("LATITUDE");
            entity.Property(e => e.Longitude).HasColumnName("LONGITUDE");
            entity.Property(e => e.NameWarehouse)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("NAME_WAREHOUSE");
        });

        modelBuilder.Entity<WorkingHour>(entity =>
        {
            entity.HasKey(e => e.IdWorkingHours);

            entity.ToTable("WORKING_HOURS");

            entity.Property(e => e.IdWorkingHours).HasColumnName("ID_WORKING_HOURS");
            entity.Property(e => e.Duration).HasColumnName("DURATION");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("END_TIME");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("START_TIME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
