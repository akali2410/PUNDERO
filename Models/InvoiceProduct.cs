using System;
using System.Collections.Generic;

namespace PUNDERO.Models;

public partial class InvoiceProduct
{
    public int IdInvoiceProduct { get; set; }

    public int OrderQuantity { get; set; }

    public int? IdInvoice { get; set; }

    public int? IdProduct { get; set; }

    public virtual Invoice? IdInvoiceNavigation { get; set; }

    public virtual Product? IdProductNavigation { get; set; }
}
