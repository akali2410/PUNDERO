using System;
using System.Collections.Generic;

namespace PUNDERO.Models;

public partial class Mobile
{
    public int IdMobile { get; set; }

    public int PhoneNumber { get; set; }

    public double? LkLongitude { get; set; }

    public double? LkLatitude { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public string Imei { get; set; } = null!;

    public virtual ICollection<MobileDriver> MobileDrivers { get; set; } = new List<MobileDriver>();
}
