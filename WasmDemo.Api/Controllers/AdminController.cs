using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WasmDemo.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("shirt-sizes")]
        public async Task<List<KeyValuePair<int, string>>> GetShirtSizes()
        {
            return new List<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Extra Small"),
                new KeyValuePair<int, string>(2, "Small"),
                new KeyValuePair<int, string>(3, "Medium"),
                new KeyValuePair<int, string>(4, "Large"),
                new KeyValuePair<int, string>(5, "Extra Large")
            };
        }
    }
}
