namespace Entity.Model;

public class RolUser : BaseModel
{

    public Rol Rol {get; set;}
    public int RolId {get; set;}

    public User User {get; set;}
    public int UserId {get; set;}

}
