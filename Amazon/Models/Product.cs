﻿using System;
using System.Collections.Generic;

namespace Amazon.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
