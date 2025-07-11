namespace Entity.DTOs.Write;

public class UserWriteDTO
{
    public int Id {get; set;}
    public int PersonId {get; set;}
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password {get; set;}
    public DateTime CreatedDate {get; set;}
    public bool Active {get; set;}
}
