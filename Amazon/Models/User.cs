﻿using System;
using System.Collections.Generic;

namespace Amazon.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Basket> Baskets { get; set; } = new List<Basket>();
}
