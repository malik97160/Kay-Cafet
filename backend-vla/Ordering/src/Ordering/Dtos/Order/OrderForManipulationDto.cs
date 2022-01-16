namespace Ordering.Dtos.Order;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class OrderForManipulationDto 
{
   public Guid CustomerId { get; set; }
   public DateTime? ExpectedPickupTime { get; set; }
   public tinyint Status { get; set; }

}