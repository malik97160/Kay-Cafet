namespace Ordering.Dtos.OrderLine;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderLineDto 
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
   public double Quantity { get; set; }
   public Guid OrderId { get; set; }
}