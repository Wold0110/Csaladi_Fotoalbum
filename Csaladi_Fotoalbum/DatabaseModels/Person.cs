using System;
using System.Collections.Generic;

namespace Csaladi_Fotoalbum.DatabaseModels;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
}
