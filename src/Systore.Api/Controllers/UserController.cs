using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Systore.Domain.Abstractions;
using Systore.Domain.Entities;

namespace Systore.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(IUserService Service, ILogger<UserController> logger)
            : base(Service, logger)
        {

        }
    }
}