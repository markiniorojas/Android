using System.ComponentModel.DataAnnotations;

namespace Entity.Model;

public class Person : GenericModel
{
    [MaxLength(100)]
    public string LastName { get; set; }

    [MaxLength(10)]
    public string TypeDocument { get; set; }

    [MaxLength(20)]
    public string DocumentNumber { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    [MaxLength(200)]
    public string Address { get; set; }

}
