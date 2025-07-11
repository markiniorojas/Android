namespace Entity.DTOs.Read;

public class UserDTO
{
    public int Id {get; set;}
    public string Username { get; set; }
    public int PersonId { get; set; }
    public string PersonName {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate {get; set;}
    public bool Active {get; set;}
    
}
