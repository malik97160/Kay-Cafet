namespace ProductManagement.Domain.Familys;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Sieve.Attributes;
using ProductManagement.Domain.Products;


public class Family : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Name { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public ICollection<Product> Products { get; set; }
}