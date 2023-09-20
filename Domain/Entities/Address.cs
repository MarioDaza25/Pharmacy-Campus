namespace Domain.Entities;

public class Address : BaseEntity
{
    //StreetName: Calle,Carrera,Avenida
    public string StreetName { get; set; }
    //StreetNumber: 25,105,48
    public string StreetNumber { get; set; }
    //StreetType: Avenida,Carril,Callejon
    public string StreetType { get; set; }
    //StreetTypeNumber: 25,105,48
    public string StreetTypeNumber { get; set; }
    public string Details { get; set; }
    public int Neighborhood_Fk { get; set; }
    public Neighborhood Neighborhood { get; set; }
    public int Person_Fk { get; set; }
    public Person Person { get; set; }

}
