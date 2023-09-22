namespace ApiPharmacy.Dtos;

public class RecipeDto
{
    public int Id { get; set; }
    public DateTime CreateDate { get; set; }
    public int Doctor_Fk { get; set; }
    public int Patient_Fk { get; set; }
}
