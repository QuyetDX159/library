using System;
using System.Collections.Generic;

namespace library.Models.Entities;

public partial class Content
{
    public int IdC { get; set; }

    public int? IdB { get; set; }

    public string? Paragraph1 { get; set; }

    public string? Paragraph2 { get; set; }

    public string? Paragraph3 { get; set; }

    public string? Img1 { get; set; }

    public string? Img2 { get; set; }

    public virtual Book? IdBNavigation { get; set; }
}
