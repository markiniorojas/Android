namespace Entity.Model;

public class Rol : GenericModel
{
    public List<RolUser> RolUsers {get; set;} 
    public List<RolFormPermission> RolFormPermissions {get; set;}
}
