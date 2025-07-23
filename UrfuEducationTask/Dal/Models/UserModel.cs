using System.ComponentModel.DataAnnotations;

namespace Dal.Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; } = new Guid();
    [Phone]
    public string Phone { get; set; }
    public string Password { get; set; }
}