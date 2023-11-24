using System.ComponentModel.DataAnnotations;

namespace WasmDemo.Shared
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required] 
        public string Password { get; set; } = string.Empty;

        public string ReturnPage {  get; set; } = string.Empty;
    }
}
