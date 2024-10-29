using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Nvc
{
    public int MaNvcs { get; set; }

    public string? MaNv { get; set; }

    public string? MaChinhSach { get; set; }

    public virtual ChinhSach? MaChinhSachNavigation { get; set; }

    public virtual Nhanvien? MaNvNavigation { get; set; }
}
