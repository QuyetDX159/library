using System;
using System.Collections.Generic;

namespace library.Models.Entities;

public partial class Book
{
    public int IdB { get; set; }

    public string? BookName { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Thumbnail { get; set; }

    public string? Author { get; set; }

    public DateTime? PublicationYear { get; set; }

    public int? IdCa { get; set; }

    public virtual ICollection<Content> Contents { get; } = new List<Content>();

    public virtual Category? IdCaNavigation { get; set; }
}
