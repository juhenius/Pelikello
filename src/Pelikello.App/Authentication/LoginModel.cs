using System.ComponentModel.DataAnnotations;

namespace Pelikello.App.Authentication;

public class LoginModel
{
  [Required(AllowEmptyStrings = false, ErrorMessage = "Passcode is required")]
  public string Passcode { get; set; } = "";
}
