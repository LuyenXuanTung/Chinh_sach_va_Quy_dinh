using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Ctqd
{
    public int MaCt { get; set; }

    public string? MaQd { get; set; }

    public string? DoiTuong { get; set; }

    public string? KetLuan { get; set; }

    public virtual QuyDinh? MaQdNavigation { get; set; }
}
