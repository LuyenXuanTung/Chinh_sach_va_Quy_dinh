using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Nvqd
{
    public int MaNvqd { get; set; }

    public string? MaNv { get; set; }

    public string? MaQuyDinh { get; set; }

    public virtual Nhanvien? MaNvNavigation { get; set; }

    public virtual QuyDinh? MaQuyDinhNavigation { get; set; }
}
