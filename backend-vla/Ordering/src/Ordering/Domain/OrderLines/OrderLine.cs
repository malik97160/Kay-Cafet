namespace Ordering.Domain.OrderLines;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using Ordering.Domain.Orders;


public class OrderLine : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public double Quantity { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
}