using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasmDemo.Shared;

namespace WasmDemo.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        [Route("get-shirt-sizes")]
        public async Task<List<KeyValuePair<int, string>>> GetShirtSizes()
        {
            try
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
            catch(Exception ex)
            {
                //log/handle
                throw;
            }
        }

        [HttpGet]
        [Route("get-people")]
        public async Task<List<PersonModel>> GetPeople()
        {
            try
            {
                return new List<PersonModel>()
            {
                new PersonModel { Name = "Jim Stevens", Birthdate = DateTime.Now.AddYears(-34), FavoriteFood = "Quiche Lorraine", ShirtSizeId = 3, SelfDescription = "I like to eat quiche and make croissants." },
                new PersonModel { Name = "Heather Smith", Birthdate = DateTime.Now.AddYears(-47), FavoriteFood = "Beef Rendang", ShirtSizeId = 2, SelfDescription = "I truly enjoy Beef Rendang." },
                new PersonModel { Name = "Beth Connell", Birthdate = DateTime.Now.AddYears(-22), FavoriteFood = "Chipotle", ShirtSizeId = 4, SelfDescription = "I play the clarinet and run marathons in my free time." }
            };
            }
            catch(Exception ex)
            {
                //log/handle
                throw;
            }
            
        }
    }
}
