namespace Entity.DTOs.Write;

public class RegisterDTO
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool Active { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string TypeDocument { get; set; }
    public string DocumentNumber { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    
    public string IdToken { get; set; }
}
