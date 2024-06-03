using System;
using System.Collections.Generic;

namespace PUNDERO.Models;

public partial class Invoice
{
    public int IdInvoice { get; set; }

    public DateTime IssueDate { get; set; }

    public int? IdStore { get; set; }

    public int? IdWarehouse { get; set; }

    public int? IdStatus { get; set; }

    public int? IdDriver { get; set; }

    public string? Note { get; set; }

    public virtual Driver? IdDriverNavigation { get; set; }

    public virtual InvoiceStatus? IdStatusNavigation { get; set; }

    public virtual Store? IdStoreNavigation { get; set; }

    public virtual Warehouse? IdWarehouseNavigation { get; set; }

    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; set; } = new List<InvoiceProduct>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}
