using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TTCSN.Models;

public partial class QlqdcsContext : DbContext
{
    public QlqdcsContext()
    {
    }

    public QlqdcsContext(DbContextOptions<QlqdcsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChinhSach> ChinhSaches { get; set; }

    public virtual DbSet<Ctc> Ctcs { get; set; }

    public virtual DbSet<Ctqd> Ctqds { get; set; }

    public virtual DbSet<Duongdan> Duongdans { get; set; }

    public virtual DbSet<NgQl> NgQls { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Nvc> Nvcs { get; set; }

    public virtual DbSet<Nvqd> Nvqds { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<QuyDinh> QuyDinhs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=XUAN\\SQLEXPRESS;Initial Catalog=QLQDCS;Integrated Security=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChinhSach>(entity =>
        {
            entity.HasKey(e => e.MaChinhSach).HasName("PK__ChinhSac__82663E302E8C2C2C");

            entity.ToTable("ChinhSach");

            entity.Property(e => e.MaChinhSach)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ChinhSach1)
                .HasMaxLength(100)
                .HasColumnName("ChinhSach");
            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.Mota).HasMaxLength(300);
            entity.Property(e => e.NgayApDung).HasColumnType("datetime");
            entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
            entity.Property(e => e.PhienBan)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Ctc>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PK__CTCS__27258E74C87885B9");

            entity.ToTable("CTCS");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.DoiTuong).HasMaxLength(200);
            entity.Property(e => e.KetLuan).HasMaxLength(300);
            entity.Property(e => e.MaCs)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaCS");

            entity.HasOne(d => d.MaCsNavigation).WithMany(p => p.Ctcs)
                .HasForeignKey(d => d.MaCs)
                .HasConstraintName("FK__CTCS__MaCS__6CD828CA");
        });

        modelBuilder.Entity<Ctqd>(entity =>
        {
            entity.HasKey(e => e.MaCt).HasName("PK__CTQD__27258E74552FC2DB");

            entity.ToTable("CTQD");

            entity.Property(e => e.MaCt).HasColumnName("MaCT");
            entity.Property(e => e.DoiTuong).HasMaxLength(200);
            entity.Property(e => e.KetLuan).HasMaxLength(300);
            entity.Property(e => e.MaQd)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaQD");

            entity.HasOne(d => d.MaQdNavigation).WithMany(p => p.Ctqds)
                .HasForeignKey(d => d.MaQd)
                .HasConstraintName("FK__CTQD__MaQD__2057CCD0");
        });

        modelBuilder.Entity<Duongdan>(entity =>
        {
            entity.HasKey(e => e.MaDuongDan).HasName("PK__DUONGDAN__14B3775ED08AD444");

            entity.ToTable("DUONGDAN");

            entity.Property(e => e.MaDuongDan)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DuongDan1)
                .HasMaxLength(100)
                .HasColumnName("DuongDan");
            entity.Property(e => e.MaChinhSach)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaQuyDinh)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaChinhSachNavigation).WithMany(p => p.Duongdans)
                .HasForeignKey(d => d.MaChinhSach)
                .HasConstraintName("FK__DUONGDAN__MaChin__1C873BEC");

            entity.HasOne(d => d.MaQuyDinhNavigation).WithMany(p => p.Duongdans)
                .HasForeignKey(d => d.MaQuyDinh)
                .HasConstraintName("FK__DUONGDAN__MaQuyD__1D7B6025");
        });

        modelBuilder.Entity<NgQl>(entity =>
        {
            entity.HasKey(e => e.MaAdm).HasName("PK__NgQL__35627110816335EA");

            entity.ToTable("NgQL");

            entity.Property(e => e.MaAdm)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.MatKhau)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TenAdmin).HasMaxLength(50);
            entity.Property(e => e.TenDangNhap)
                .HasMaxLength(50)
                .IsFixedLength();
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("PK__NHANVIEN__2725D70AFF53F320");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.ChucVu).HasMaxLength(30);
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.GioiTinh).HasMaxLength(10);
            entity.Property(e => e.HoTen).HasMaxLength(50);
            entity.Property(e => e.HoTro).HasMaxLength(300);
            entity.Property(e => e.Loi).HasMaxLength(300);
            entity.Property(e => e.MaNguoiSua)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MatKhau).HasMaxLength(20);
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(100);
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsFixedLength()
                .HasColumnName("SDT");
            entity.Property(e => e.TruyCap).HasColumnType("datetime");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__NHANVIEN__MaPhon__72910220");
        });

        modelBuilder.Entity<Nvc>(entity =>
        {
            entity.HasKey(e => e.MaNvcs).HasName("PK__NVCS__1F218687090FA92F");

            entity.ToTable("NVCS");

            entity.Property(e => e.MaChinhSach)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaNV");

            entity.HasOne(d => d.MaChinhSachNavigation).WithMany(p => p.Nvcs)
                .HasForeignKey(d => d.MaChinhSach)
                .HasConstraintName("FK__NVCS__MaChinhSac__76619304");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Nvcs)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__NVCS__MaNV__756D6ECB");
        });

        modelBuilder.Entity<Nvqd>(entity =>
        {
            entity.HasKey(e => e.MaNvqd).HasName("PK__NVQD__1F205F5568A0D596");

            entity.ToTable("NVQD");

            entity.Property(e => e.MaNv)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaQuyDinh)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.Nvqds)
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK__NVQD__MaNV__18B6AB08");

            entity.HasOne(d => d.MaQuyDinhNavigation).WithMany(p => p.Nvqds)
                .HasForeignKey(d => d.MaQuyDinh)
                .HasConstraintName("FK__NVQD__MaQuyDinh__19AACF41");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__PhongBan__20BD5E5B83A75916");

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPhong)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Mota).HasMaxLength(300);
            entity.Property(e => e.TenPhong).HasMaxLength(20);
        });

        modelBuilder.Entity<QuyDinh>(entity =>
        {
            entity.HasKey(e => e.MaQuyDinh).HasName("PK__QuyDinh__F7917049B0E69B19");

            entity.ToTable("QuyDinh");

            entity.Property(e => e.MaQuyDinh)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DuongDan).HasMaxLength(255);
            entity.Property(e => e.Mota).HasMaxLength(300);
            entity.Property(e => e.NgayApDung).HasColumnType("datetime");
            entity.Property(e => e.NgayHetHan).HasColumnType("datetime");
            entity.Property(e => e.PhienBan)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.QuyDinh1)
                .HasMaxLength(100)
                .HasColumnName("QuyDinh");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
