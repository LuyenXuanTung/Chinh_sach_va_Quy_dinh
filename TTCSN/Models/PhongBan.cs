using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class PhongBan
{
    public string MaPhong { get; set; } = null!;

    public string? TenPhong { get; set; }

    public string? Mota { get; set; }

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
