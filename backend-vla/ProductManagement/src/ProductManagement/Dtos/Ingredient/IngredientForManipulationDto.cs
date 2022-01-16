namespace ProductManagement.Dtos.Ingredient;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class IngredientForManipulationDto 
{
   public Guid ParentProductId { get; set; }
   public Guid IngredientProductId { get; set; }
   public string Unit { get; set; }
   public double Amount { get; set; }
}