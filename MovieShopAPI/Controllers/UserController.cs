using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetMoviesPurchasedByUser()
        {
            // we need to get the userId from the token, using HttpContext
            return Ok("Purchases would be here");
        }
    }
}
