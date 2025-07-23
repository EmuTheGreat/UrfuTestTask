using System.ComponentModel.DataAnnotations;
using Api.Attributes;

namespace Api.Controllers.AccountApiController.Request;

public record RegisterRequest()
{
    [PhoneValidation]
    public string Phone {  get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым.")]
    [StringLength(20, ErrorMessage = "Пароль не может быть больше 20 символов.")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Поле не может быть пустым.")]
    [StringLength(20, ErrorMessage = "Пароль не может быть больше 20 символов.")]
    [Compare("Password", ErrorMessage = "Пароли не совпадают")]
    public string ConfirmPassword { get; set; }
}