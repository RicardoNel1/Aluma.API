using Aluma.API.RepoWrapper;
using AutoMapper;
using DataService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Aluma.API.Controllers
{
    [ApiController, Route("api/[controller]"), Authorize]
    public class DocumentController : ControllerBase
    {
        private readonly IWrapper _repo;

        public DocumentController(IWrapper repo, IMapper mapper)
        {
            _repo = repo;
        }

        [HttpGet("application/list")]
        public IActionResult ApplicationDocsList(ApplicationDto dto)
        {
            try
            {
                //not actual documents just list
                dto.Documents = _repo.ApplicationDocuments.GetDocumentsList(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("application/list/download")]
        public IActionResult ApplicationDocsDownload(ApplicationDto dto)
        {
            try
            {
                // actual documents, just list of base64 strings
                var docList = _repo.ApplicationDocuments.GetDocuments(dto);

                return Ok(docList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("application/list/id"), AllowAnonymous]
        public IActionResult GetApplicationDocument(ApplicationDocumentDto dto)
        {
            try
            {
                var doc = _repo.ApplicationDocuments.GetDocument(dto);

                return Ok(doc);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list")]
        public IActionResult UserDocsList(UserDto dto)
        {
            try
            {
                //not actual documents just list
                dto.Documents = _repo.UserDocuments.GetDocumentsList(dto);

                return Ok(dto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list/download")]
        public IActionResult UserDocsDownload(UserDto dto)
        {
            try
            {
                // actual documents, just list of base64 strings
                var docList = _repo.UserDocuments.GetDocuments(dto.Id);

                return Ok(docList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("user/list/id"), AllowAnonymous]
        public IActionResult GetUserDocument(UserDocumentDto dto)
        {
            try
            {
                var doc = _repo.UserDocuments.GetDocument(dto);

                return Ok(doc);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost("user/upload")]
        public IActionResult UserUploadMultipleDocuments([FromBody] List<UserDocumentDto> dto)
        {
            try
            {
                foreach (var doc in dto)
                {
                    _repo.UserDocuments.UploadDocument(doc);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}