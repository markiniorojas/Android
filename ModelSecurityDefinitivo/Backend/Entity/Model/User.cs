using System.ComponentModel.DataAnnotations;

namespace Entity.Model;

public class User : BaseModel
{
    [MaxLength(100)]
    public string Username { get; set; }

    [MaxLength(100)]
    [EmailAddress] // (opcional, si usas validaciones del lado del modelo)
    public string Email { get; set; }

    [MaxLength(255)]
    public string Password { get; set; }
    public DateTime CreatedDate {get; set;}
    public bool Active {get; set;}

    public Person Person {get; set;}
    public int PersonId {get; set;}

    //Probablemente de error
    public List<RolUser> RolUsers {get; set; }
}
 