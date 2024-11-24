using System;
using System.Collections.Generic;

namespace Amazon.Models;

public partial class Basket
{
    public int Id { get; set; }

    public int UsersId { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual User Users { get; set; } = null!;
}
