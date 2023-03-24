using System;
using System.Collections.Generic;

namespace library.Models.Entities;

public partial class Category
{
    public int IdCa { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Book> Books { get; } = new List<Book>();
}
