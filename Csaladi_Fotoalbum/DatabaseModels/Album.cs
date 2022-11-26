using System;
using System.Collections.Generic;

namespace Csaladi_Fotoalbum.DatabaseModels;

public partial class Album
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Date { get; set; }

    public int Location { get; set; }

    public int Color { get; set; }
}
