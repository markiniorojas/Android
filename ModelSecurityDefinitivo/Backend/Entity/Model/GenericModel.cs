using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Model;

public class GenericModel : BaseModel
{
    [MaxLength(255)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
}
