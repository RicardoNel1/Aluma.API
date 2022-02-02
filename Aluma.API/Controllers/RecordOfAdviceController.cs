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
        private readonly IWrapper _repo;

        public RecordOfAdviceController(IWrapper repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public IActionResult CreateAdvice([FromBody] RecordOfAdviceDto dto)
        {
            try
            {
                bool adviceExist = _repo.RecordOfAdvice.DoesApplicationHaveRecordOfAdice(dto);
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

        [HttpPut]
        public IActionResult UpdateAdvice([FromBody] RecordOfAdviceDto dto)
        {
            try
            {
                bool adviceExist = _repo.RecordOfAdvice.DoesApplicationHaveRecordOfAdice(dto);
                if (adviceExist)
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

        [HttpGet]
        public IActionResult GetAdvice([FromBody] RecordOfAdviceDto dto)
        {
            try
            {
                RecordOfAdviceDto advice = _repo.RecordOfAdvice.GetRecordOfAdvice(dto);

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
    }
}