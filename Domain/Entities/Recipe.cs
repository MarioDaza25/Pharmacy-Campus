namespace Domain.Entities;

public class Recipe : BaseEntity
{
    public DateTime CreateDate { get; set; }
    public int Doctor_Fk { get; set; }
    public Person Doctor { get; set; }
    public int Patient_Fk { get; set; }
    public Person Patient { get; set; }
    public ICollection<RecipeProduct> RecipeProducts { get; set; }

}
