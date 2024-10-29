using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Nhanvien
{
    public string MaNv { get; set; } = null!;

    public string? MatKhau { get; set; }

    public string? HoTen { get; set; }

    public DateTime? NgaySinh { get; set; }

    public string? GioiTinh { get; set; }

    public string? ChucVu { get; set; }

    public string? DiaChi { get; set; }

    public string? Email { get; set; }

    public string? Sdt { get; set; }

    public DateTime? TruyCap { get; set; }

    public double? Luong { get; set; }

    public double? Thuong { get; set; }

    public double? Phat { get; set; }

    public double? TangCa { get; set; }

    public double? Nghi { get; set; }

    public double? KinhNghiem { get; set; }

    public string? Loi { get; set; }

    public string? HoTro { get; set; }

    public string? MaNguoiSua { get; set; }

    public string? NguoiSua { get; set; }

    public DateTime? NgaySua { get; set; }

    public string? MaPhong { get; set; }

    public virtual PhongBan? MaPhongNavigation { get; set; }

    public virtual ICollection<Nvc> Nvcs { get; set; } = new List<Nvc>();

    public virtual ICollection<Nvqd> Nvqds { get; set; } = new List<Nvqd>();
}
