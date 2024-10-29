using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Ctc
{
    public int MaCt { get; set; }

    public string? MaCs { get; set; }

    public string? DoiTuong { get; set; }

    public string? KetLuan { get; set; }

    public virtual ChinhSach? MaCsNavigation { get; set; }
}
