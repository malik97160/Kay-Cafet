namespace ProductManagement.Domain.Products;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ProductManagement.Domain.Families;
using ProductManagement.Domain.Ingredients;


public class Product : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Family")]
    public Guid FamilyId { get; set; }
    public Family Family { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Description { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public number UnitPrice { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public number QuantityOnHand { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string ImageLink { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<Ingredient> Ingredients { get; set; }
}