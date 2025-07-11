using Microsoft.EntityFrameworkCore.Storage;

namespace Entity.Model;

public class FormModule : BaseModel
{

    public Form Form {get; set;}
    public int FormId {get; set;}

    public Module Module {get; set;}
    public int ModuleId {get; set;}

}
