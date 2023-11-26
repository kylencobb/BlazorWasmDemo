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
        public string? Token { get; set; }
    }
}
