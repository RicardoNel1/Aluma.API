using Aluma.API.RepoWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IWrapper _repo;

        public AdminController(IWrapper repo)
        {
            _repo = repo;
        }
    }
}