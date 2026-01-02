using System;
using System.Collections.Generic;

namespace Resources.Models;

public partial class Resource
{
    public int ResourceId { get; set; }

    public string Name { get; set; } = null!;

    public int? GostId { get; set; }

    public string? Characteristics { get; set; }

    public string? Unit { get; set; }

    public virtual Gost? Gost { get; set; }

    public override string ToString()
    {
        return $" Название: {Name}, Характеристика: {Characteristics}, Мера: {Unit}, {Gost?.Name}";
    }
}
