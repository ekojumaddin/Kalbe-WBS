using System;
using System.Collections.Generic;

namespace WBSBE.Common.Entity.WBS;

public partial class TestDb2
{
    public int Id2 { get; set; }

    public int Id1 { get; set; }

    public string? Nama2 { get; set; }

    public DateTime? Tanggal { get; set; }

    public bool? Checklist { get; set; }

    public virtual TestDb Id1Navigation { get; set; } = null!;
}
