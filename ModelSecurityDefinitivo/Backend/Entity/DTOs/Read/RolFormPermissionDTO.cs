namespace Entity.DTOs.Read;

public class RolFormPermissionDTO
{
    public int Id {get; set;}

    public bool IsDeleted { get; set; }

    public int RolId {get; set;}
    public string RolName {get; set;}

    public int FormId {get; set;}
    public string FormName {get; set;}
    public int PermissionId {get; set;}
    public string PermissionName {get; set;}
}
