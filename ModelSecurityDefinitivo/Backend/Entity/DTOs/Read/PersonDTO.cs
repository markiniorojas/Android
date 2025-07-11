namespace Entity.DTOs.Read;

public class PersonDTO
{
    public int Id {get; set;}
    public string Name {get; set;}
    public string LastName {get; set;}
    public string TypeDocument {get; set;}
    public string DocumentNumber {get; set;}
    public string Phone {get; set;}
    public string Address {get; set;}
    public bool IsDeleted { get; set; }
}
