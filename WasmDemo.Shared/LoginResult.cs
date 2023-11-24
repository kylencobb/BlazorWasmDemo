using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasmDemo.Shared
{
    public class LoginResult
    {
        public bool IsSuccess { get; set; } = false;
        public string? Error {  get; set; }
        public string? Token { get; set; }
        public string ReturnPage { get; set; } = string.Empty;
    }
}
