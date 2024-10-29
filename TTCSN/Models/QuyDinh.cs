using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class QuyDinh
{
    public string MaQuyDinh { get; set; } = null!;

    public string? QuyDinh1 { get; set; }

    public DateTime? NgayApDung { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public string? DuongDan { get; set; }

    public string? Mota { get; set; }

    public string? PhienBan { get; set; }

    public virtual ICollection<Ctqd> Ctqds { get; set; } = new List<Ctqd>();

    public virtual ICollection<Duongdan> Duongdans { get; set; } = new List<Duongdan>();

    public virtual ICollection<Nvqd> Nvqds { get; set; } = new List<Nvqd>();
}
