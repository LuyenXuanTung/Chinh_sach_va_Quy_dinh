using System;
using System.Collections.Generic;

namespace TTCSN.Models;

public partial class Duongdan
{
    public string MaDuongDan { get; set; } = null!;

    public string? DuongDan1 { get; set; }

    public string? MaChinhSach { get; set; }

    public string? MaQuyDinh { get; set; }

    public virtual ChinhSach? MaChinhSachNavigation { get; set; }

    public virtual QuyDinh? MaQuyDinhNavigation { get; set; }
}
