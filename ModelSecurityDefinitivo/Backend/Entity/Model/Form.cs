using System.ComponentModel.DataAnnotations;

namespace Entity.Model;

public class Form : GenericModel
{
    [MaxLength(2048)]
    public string Url {get; set;}

    public List<FormModule> FormModules {get; set;}
    public List<RolFormPermission> RolFormPermissions {get; set;}

}
