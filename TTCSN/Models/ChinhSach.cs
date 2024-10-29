using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class ChinhSach
{
    public string MaChinhSach { get; set; } = null!;

    public string? ChinhSach1 { get; set; }

    public DateTime? NgayApDung { get; set; }

    public DateTime? NgayHetHan { get; set; }

    public string? DuongDan { get; set; }

    public string? Mota { get; set; }

    public string? PhienBan { get; set; }

    public virtual ICollection<Ctc> Ctcs { get; set; } = new List<Ctc>();

    public virtual ICollection<Duongdan> Duongdans { get; set; } = new List<Duongdan>();

    public virtual ICollection<Nvc> Nvcs { get; set; } = new List<Nvc>();
}
