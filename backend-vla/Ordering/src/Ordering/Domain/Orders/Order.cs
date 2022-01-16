namespace Ordering.Domain.Orders;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using Ordering.Domain.OrderLines;


public class Order : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public Guid CustomerId { get; set; } = Guid.NewGuid();

    [Sieve(CanFilter = true, CanSort = true)]
    public DateTime? ExpectedPickupTime { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public tinyint Status { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<OrderLine> OrderLines { get; set; }
}