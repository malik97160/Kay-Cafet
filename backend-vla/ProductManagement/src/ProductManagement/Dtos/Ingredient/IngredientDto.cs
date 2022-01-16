namespace ProductManagement.Dtos.Ingredient;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class IngredientDto 
{
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedOn { get; set; }
    public string? LastModifiedBy { get; set; }
   public Guid ParentProductId { get; set; }
   public Guid IngredientProductId { get; set; }
   public string Unit { get; set; }
   public double Amount { get; set; }
}