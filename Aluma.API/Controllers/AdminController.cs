using Aluma.API.RepoWrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public AdminController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors
    }
}