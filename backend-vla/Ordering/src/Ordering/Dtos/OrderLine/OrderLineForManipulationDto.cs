namespace Ordering.Dtos.OrderLine;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class OrderLineForManipulationDto 
{
   public double Quantity { get; set; }
   public Guid OrderId { get; set; }
}