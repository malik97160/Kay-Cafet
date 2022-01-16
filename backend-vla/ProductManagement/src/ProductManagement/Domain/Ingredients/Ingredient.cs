namespace ProductManagement.Domain.Ingredients;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ProductManagement.Domain.Products;
using ProductManagement.Domain.Products;


public class Ingredient : BaseEntity
{
    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Product")]
    public Guid ParentProductId { get; set; }
    public Product Product { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    [ForeignKey("Product")]
    public Guid IngredientProductId { get; set; }
    public Product Product { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string Unit { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public double Amount { get; set; }
}