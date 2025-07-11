namespace Entity.DTOs.Read;

public class RolUserDTO
{
    public int Id {get; set;}

    public int RolId {get; set;}
    public string RolName {get; set;}

    public bool IsDeleted { get; set; }
    public int UserId {get; set;}
    public string UserEmail {get; set;}
}
