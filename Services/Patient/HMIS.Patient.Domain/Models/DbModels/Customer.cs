using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModels;

public partial class Customer
{
    public byte[] CustomerId { get; set; } = null!;

    public string Name { get; set; } = null!;
}
