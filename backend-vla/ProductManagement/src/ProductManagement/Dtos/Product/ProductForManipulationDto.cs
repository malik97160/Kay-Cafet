namespace ProductManagement.Dtos.Product;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class ProductForManipulationDto 
{
   public string Name { get; set; }
   public Guid FamilyId { get; set; }
   public string Description { get; set; }
   public number UnitPrice { get; set; }
   public number QuantityOnHand { get; set; }
   public string ImageLink { get; set; }

}