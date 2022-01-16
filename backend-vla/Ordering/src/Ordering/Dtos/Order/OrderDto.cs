namespace Ordering.Dtos.Order;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class OrderDto 
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
   public Guid CustomerId { get; set; }
   public DateTime? ExpectedPickupTime { get; set; }
   public tinyint Status { get; set; }

}