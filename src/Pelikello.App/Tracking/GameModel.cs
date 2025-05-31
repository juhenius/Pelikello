using System.ComponentModel.DataAnnotations;

namespace Pelikello.App.Tracking;

public class GameModel
{
  [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
  public string Name { get; set; } = "";

  [Required(AllowEmptyStrings = false, ErrorMessage = "Pattern is required")]
  public string Pattern { get; set; } = "";
}
