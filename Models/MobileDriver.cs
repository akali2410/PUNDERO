﻿using System;
using System.Collections.Generic;

namespace PUNDERO.Models;

public partial class MobileDriver
{
    public int IdMobile { get; set; }

    public int IdDriver { get; set; }

    public DateTime AssignmentStartDate { get; set; }

    public DateTime AssignmentEndDate { get; set; }

    public int? IdAssignmentType { get; set; }

    public virtual AssignmentType? IdAssignmentTypeNavigation { get; set; }

    public virtual Driver IdDriverNavigation { get; set; } = null!;

    public virtual Mobile IdMobileNavigation { get; set; } = null!;
}
