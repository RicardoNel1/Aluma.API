using Aluma.API.RepoWrapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class RecordOfAdviceController : ControllerBase
    {
        #region Private Fields

        private readonly IWrapper _repo;

        #endregion Private Fields

        #region Public Constructors

        public RecordOfAdviceController(IWrapper repo)
        {
            _repo = repo;
        }

        #endregion Public Constructors

        #region Public Methods

        [HttpPost, AllowAnonymous]
        public IActionResult CreateAdvice([FromBody] RecordOfAdviceDto dto)
        {
            try
            {
                bool adviceExist = _repo.RecordOfAdvice.DoesApplicationHaveRecordOfAdice(dto.ApplicationId);
                if (adviceExist)
                {
                    return BadRequest("ROA Exists");
                }

                RecordOfAdviceDto advice = _repo.RecordOfAdvice.CreateRecordOfAdvice(dto);

                return Ok(advice);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public IActionResult DeleteAdvice(RecordOfAdviceDto dto)
        {
            try
            {
                bool isDeleted = _repo.RecordOfAdvice.DeleteRecordOfAdvice(dto);
                if (!isDeleted)
                {
                    return BadRequest("RecordOfAdvice Not Deleted");
                }
                return Ok("RecordOfAdvice Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetAdvice(int applicationId)
        {
            try
            {
                RecordOfAdviceDto advice = _repo.RecordOfAdvice.GetRecordOfAdvice(applicationId);

                return Ok(advice);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult UpdateAdvice([FromBody] RecordOfAdviceDto dto)
        {
            try
            {
                bool adviceExist = _repo.RecordOfAdvice.DoesApplicationHaveRecordOfAdice(dto.ApplicationId);
                if (!adviceExist)
                {
                    return BadRequest("ROA Does Not Exist");
                }

                RecordOfAdviceDto advice = _repo.RecordOfAdvice.UpdateRecordOfAdvice(dto);

                return Ok(advice);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        #endregion Public Methods
    }
}