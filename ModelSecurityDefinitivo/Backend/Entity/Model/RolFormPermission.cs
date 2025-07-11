namespace Entity.Model;

public class RolFormPermission : BaseModel
{

    public Rol Rol {get; set;}
    public int RolId {get; set;}

    public Form Form {get; set;}
    public int FormId {get; set;}
    
    public Permission Permission {get; set;}
    public int PermissionId {get; set;}

}
