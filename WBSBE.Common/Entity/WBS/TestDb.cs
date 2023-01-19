using System;
using System.Collections.Generic;

namespace WBSBE.Common.Entity.WBS;

public partial class TestDb
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    public string? Test { get; set; }

    public virtual ICollection<TestDb2> TestDb2s { get; } = new List<TestDb2>();
}
