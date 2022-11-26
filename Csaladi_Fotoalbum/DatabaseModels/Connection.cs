using System;
using System.Collections.Generic;

namespace Csaladi_Fotoalbum.DatabaseModels;

public partial class Connection
{
    public int Id { get; set; }

    public int Person { get; set; }

    public int Album { get; set; }
}
